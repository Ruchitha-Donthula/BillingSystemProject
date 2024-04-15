using System;
using System.Collections.Generic;
using System.Linq;
using BillingSystemDataModel;
using BillingSystemDataAccess;

namespace BillingSystemBusiness
{
    public class InvoiceBusiness
    {
        public Invoice CreateInvoice(BillAccount billAccount)
        {
            var currentDate = DateTime.Now;
            var installmentSummaries = new InstallmentSummaryDataAccess().GetInstallmentSummariesByBillAccountId(billAccount.BillAccountId);
            var pendingInstallments = new List<Installment>();

            foreach (var summary in installmentSummaries)
            {
                pendingInstallments.AddRange(summary.Installments
                    .Where(installment => installment.InstallmentSendDate.Date == currentDate.Date));
            }

            if (!pendingInstallments.Any())
            {
                return null;
            }

            Invoice invoice = new Invoice
            {
                InvoiceNumber = GenerateInvoiceNumber(),
                InvoiceDate = DateTime.Now.Date,
                SendDate = DateTime.Now.Date,
                InvoiceAmount = CalculateTotalAmount(pendingInstallments),
                BillAccountId = billAccount.BillAccountId,
                Status = "Invoice Sent"
            };

            new InvoiceDataAccess().AddInvoice(invoice);

            foreach (var installment in pendingInstallments)
            {
                new InstallmentDataAccess().ActivateInstallmentStatus(installment);
                var invoiceInstallment = new InvoiceInstallment
                {
                    InvoiceId = invoice.InvoiceId,
                    InstallmentId = installment.InstallmentId
                };

                new InvoiceInstallmentDataAccess().AddInvoiceInstallment(invoiceInstallment);
            }

            return invoice;
        }

        private string GenerateInvoiceNumber()
        {
            int nextSequenceNumber = GetNextSequenceNumberFromDatabase();
            string invoiceNumber = $"IN{nextSequenceNumber:D6}";
            return invoiceNumber;
        }

        private int GetNextSequenceNumberFromDatabase()
        {
            int nextSequenceNumberFromDatabase = new InvoiceDataAccess().GetNextSequenceNumber();
            return nextSequenceNumberFromDatabase;
        }

        public double CalculateTotalAmount(List<Installment> pendingInstallments)
        {
            double totalAmount = (double)pendingInstallments.Sum(installment => (double)installment.BalanceAmount);
            return totalAmount;
        }
    }
}
