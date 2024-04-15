using BillingSystemDataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingSystemDataAccess
{
    public class InstallmentDataAccess
    {
        private readonly BillingSystemEDMContainer _dbContext;

        public InstallmentDataAccess()
        {
            _dbContext = new BillingSystemEDMContainer();
        }

        public void AddInstallment(Installment installment)
        {
            _dbContext.Installments.Add(installment);
            _dbContext.SaveChanges();
        }

        public void UpdateInstallment(Installment installment)
        {
            var existingInstallment = _dbContext.Installments.Find(installment.InstallmentId);
            if (existingInstallment != null)
            {
                // Update properties
                existingInstallment.InstallmentSequenceNumber = installment.InstallmentSequenceNumber;
                existingInstallment.InstallmentSendDate = installment.InstallmentSendDate;
                existingInstallment.InstallmentDueDate = installment.InstallmentDueDate;
                existingInstallment.DueAmount = installment.DueAmount;
                existingInstallment.PaidAmount = installment.PaidAmount;
                existingInstallment.BalanceAmount = installment.BalanceAmount;
                existingInstallment.InvoiceStatus = installment.InvoiceStatus;
                existingInstallment.InstallmentSummaryId = installment.InstallmentSummaryId;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteInstallmentById(int id)
        {
            var installment = _dbContext.Installments.Find(id);
            if (installment != null)
            {
                _dbContext.Installments.Remove(installment);
                _dbContext.SaveChanges();
            }
        }

        public Installment GetInstallmentById(int id)
        {
            return _dbContext.Installments.Find(id);
        }

        public List<Installment> GetAllInstallments()
        {
            return _dbContext.Installments.ToList();
        }
        public void ActivateInstallmentStatus(Installment installment)
        {
           Installment installmentToBeActivated = GetInstallmentById(installment.InstallmentId);
           installmentToBeActivated.InvoiceStatus = "Billed";
            _dbContext.SaveChanges();
        }
        public List<Installment> GetInstallmentsByInvoiceNumber(string invoiceNumber)
        {
            List<Installment> installments = new List<Installment>();

            using (var dbContext = new BillingSystemEDMContainer())
            {
                var query = from invoiceInstallment in dbContext.InvoiceInstallments
                            join invoice in dbContext.Invoices
                            on invoiceInstallment.InvoiceId equals invoice.InvoiceId
                            join installment in dbContext.Installments
                            on invoiceInstallment.InstallmentId equals installment.InstallmentId
                            where invoice.InvoiceNumber == invoiceNumber
                            select installment;

                installments = query.ToList();
            }

            return installments;
        }
        public List<Installment> GetFutureInstallmentsByBillAccountId(int billAccountId)
        {
            using (var dbContext = new BillingSystemEDMContainer())
            {
                var installmentSummaries = dbContext.InstallmentSummaries
                    .Where(summary => summary.BillAccountId == billAccountId)
                    .ToList();

                var futureInstallments = new List<Installment>();
                foreach (var summary in installmentSummaries)
                {
                    var futureInstallmentsForSummary = dbContext.Installments
                        .Where(installment => installment.InstallmentSummaryId == summary.InstallmentSummaryId && installment.InstallmentSendDate > DateTime.Now)
                        .ToList();

                    futureInstallments.AddRange(futureInstallmentsForSummary);
                }

                return futureInstallments;
            }
        }
    }
}
