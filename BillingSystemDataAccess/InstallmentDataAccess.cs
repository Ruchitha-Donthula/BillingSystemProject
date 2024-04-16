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
            try
            {
                _dbContext.Installments.Add(installment);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding an installment.", ex);
            }
        }

        public void UpdateInstallment(Installment installment)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating an installment.", ex);
            }
        }

        public void DeleteInstallmentById(int id)
        {
            try
            {
                var installment = _dbContext.Installments.Find(id);
                if (installment != null)
                {
                    _dbContext.Installments.Remove(installment);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting an installment.", ex);
            }
        }

        public Installment GetInstallmentById(int id)
        {
            try
            {
                return _dbContext.Installments.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving an installment by Id.", ex);
            }
        }

        public List<Installment> GetAllInstallments()
        {
            try
            {
                return _dbContext.Installments.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all installments.", ex);
            }
        }

        public void ActivateInstallmentStatus(Installment installment)
        {
            try
            {
                Installment installmentToBeActivated = GetInstallmentById(installment.InstallmentId);
                installmentToBeActivated.InvoiceStatus = "Billed";
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while activating installment status.", ex);
            }
        }

        public List<Installment> GetInstallmentsByInvoiceNumber(string invoiceNumber)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving installments by invoice number.", ex);
            }
        }

        public List<Installment> GetFutureInstallmentsByBillAccountId(int billAccountId)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving future installments by BillAccountId.", ex);
            }
        }
    }
}
