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
           installmentToBeActivated.InvoiceStatus = "Active";
            _dbContext.SaveChanges();
        }
    }
}
