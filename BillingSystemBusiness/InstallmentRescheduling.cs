using System;
using System.Collections.Generic;
using BillingSystemDataAccess;
using BillingSystemDataModel;
using System.Linq;

namespace BillingSystemBusiness
{
    public class InstallmentRescheduling
    {
        public void OnChangeOfBillAccountDueDay(BillAccount billaccount, int newDueDay)
        {
            try
            {
                // Update the due day in the BillAccount
                BillAccount billAccount = new BillAccountDataAccess().GetBillAccountById(billaccount.BillAccountId);
                billAccount.DueDay = newDueDay;
                new BillAccountDataAccess().UpdateBillAccount(billAccount);

                // Retrieve all old InstallmentSummaries associated with the given BillAccount
                List<InstallmentSummary> oldSummaries = new InstallmentDataAccess().GetInstallmentSummariesByBillAccountId(billAccount.BillAccountId);

                // Mark old InstallmentSummaries as inactive and create new summaries with the updated due day
                foreach (var oldSummary in oldSummaries)
                {
                    oldSummary.Status = ApplicationConstants.INSTALLMENT_SUMMARY_INACTIVE_STATUS;
                    new InstallmentDataAccess().UpdateInstallmentSummary(oldSummary);

                    // Create new InstallmentSummary
                    InstallmentSummary newSummary = new InstallmentSummary
                    {
                        PolicyNumber = oldSummary.PolicyNumber,
                        Status = ApplicationConstants.INSTALLMENT_SUMMARY_ACTIVE_STATUS,
                        BillAccountId = oldSummary.BillAccountId,
                    };
                    new InstallmentDataAccess().AddInstallmentSummary(newSummary);

                    // Copy billed Installments to the new InstallmentSummary and reschedule pending Installments
                    RescheduleInstallments(oldSummary, newSummary, newDueDay);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the change of BillAccount due day.", ex);
            }
        }

        private void RescheduleInstallments(InstallmentSummary oldSummary, InstallmentSummary newSummary, int newDueDay)
        {
            // Retrieve billed Installments associated with the old InstallmentSummary
            List<Installment> billedInstallments = new InstallmentDataAccess().GetBilledInstallmentsBySummaryId(oldSummary.InstallmentSummaryId);

            // Copy billed Installments to the new InstallmentSummary
            foreach (var billedInstallment in billedInstallments)
            {
                billedInstallment.InstallmentSummaryId = newSummary.InstallmentSummaryId;
                new InstallmentDataAccess().AddInstallment(billedInstallment);
            }

            // Retrieve pending Installments associated with the old InstallmentSummary
            List<Installment> pendingInstallments = new InstallmentDataAccess().GetPendingInstallmentsBySummaryId(oldSummary.InstallmentSummaryId);

            // Reschedule pending Installments with the new due day
            foreach (var pendingInstallment in pendingInstallments)
            {
                string payplan = new BillAccountPolicyDataAccess().GetPayPlanByPolicyNumber(oldSummary.PolicyNumber);
                pendingInstallment.InstallmentDueDate = new InstallmentBusiness().CalculateDueDate(pendingInstallment.InstallmentSequenceNumber, payplan, newDueDay);
                pendingInstallment.InstallmentSendDate = new InstallmentBusiness().CalculateSendDate(pendingInstallment.InstallmentDueDate);
                pendingInstallment.InvoiceStatus = ApplicationConstants.INSTALLMENT_INVOICE_STATUS_PENDING;
                pendingInstallment.InstallmentSummaryId = newSummary.InstallmentSummaryId;
                new InstallmentDataAccess().AddInstallment(pendingInstallment);
            }
        }
    }
}
