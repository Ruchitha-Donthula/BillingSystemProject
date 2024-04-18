// <copyright file="InvoiceBusiness.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BillingSystemBusiness
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BillingSystemDataAccess;
    using BillingSystemDataModel;

    /// <summary>
    /// Provides business logic for creating invoices.
    /// </summary>
    public class InvoiceBusiness
    {
        /// <summary>
        /// Creates an invoice for the specified bill account.
        /// </summary>
        /// <param name="billAccount">The bill account for which to create the invoice.</param>
        /// <returns>The created invoice.</returns>
        public Invoice CreateInvoice(BillAccount billAccount)
        {
            try
            {
                var currentDate =new DateTime(2025,03,11);
                var installmentSummaries = new InstallmentDataAccess().GetInstallmentSummariesByBillAccountId(billAccount.BillAccountId);
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
                    InvoiceNumber = this.GenerateInvoiceNumber(),
                    InvoiceDate = DateTime.Now.Date,
                    SendDate = DateTime.Now.Date,
                    InvoiceAmount = this.CalculateTotalAmount(pendingInstallments),
                    BillAccountId = billAccount.BillAccountId,
                    Status = ApplicationConstants.INVOICE_STATUS_SENT,
                };

                new InvoiceDataAccess().AddInvoice(invoice);

                foreach (var installment in pendingInstallments)
                {
                    new InstallmentDataAccess().ActivateInstallmentStatus(installment);
                    var invoiceInstallment = new InvoiceInstallment
                    {
                        InvoiceId = invoice.InvoiceId,
                        InstallmentId = installment.InstallmentId,
                    };

                    new InvoiceDataAccess().AddInvoiceInstallment(invoiceInstallment);
                }

                return invoice;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating invoice.", ex);
            }
        }

        /// <summary>
        /// Generates a unique invoice number.
        /// </summary>
        /// <returns>The generated invoice number.</returns>
        private string GenerateInvoiceNumber()
        {
            try
            {
                int nextSequenceNumber = this.GetNextSequenceNumberFromDatabase();
                string invoiceNumber = $"IN{nextSequenceNumber:D6}";
                return invoiceNumber;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating invoice number.", ex);
            }
        }

        /// <summary>
        /// Retrieves the next sequence number for generating invoice numbers from the database.
        /// </summary>
        /// <returns>The next sequence number.</returns>
        private int GetNextSequenceNumberFromDatabase()
        {
            try
            {
                int nextSequenceNumberFromDatabase = new InvoiceDataAccess().GetNextSequenceNumber();
                return nextSequenceNumberFromDatabase;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving next sequence number from database.", ex);
            }
        }

        /// <summary>
        /// Calculates the total amount for the invoice based on the pending installments.
        /// </summary>
        /// <param name="pendingInstallments">The list of pending installments.</param>
        /// <returns>The total amount for the invoice.</returns>
        private double CalculateTotalAmount(List<Installment> pendingInstallments)
        {
            try
            {
                double totalAmount = (double)pendingInstallments.Sum(installment => (double)installment.BalanceAmount);
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while calculating total amount.", ex);
            }
        }
    }
}
