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

        public void OnChangeOfPayPlan(BillAccount billAccount, BillAccountPolicy newPayPlan)
        {
            InstallmentSummary newInstallmentSummary = CreateNewInstallmentSummary(billAccount, newPayPlan);

            new InstallmentDataAccess().AddInstallmentSummary(newInstallmentSummary);

            CopyPaidInstallmentsToNewSchedule(billAccount, newPayPlan, newInstallmentSummary);

           double updatedPremium=GetRemainingPremium(billAccount,newPayPlan);

            // Add remaining installments based on the new pay plan
            AddRemainingInstallmentsToNewSchedule(newInstallmentSummary, newPayPlan, updatedPremium, billAccount.DueDay);

            // Mark the previous installment schedule as 'Expired'
            MarkPreviousScheduleAsExpired(billAccount,newPayPlan);
        }

        private double GetRemainingPremium(BillAccount billAccount,BillAccountPolicy newPayPlan)
        {
            List<InstallmentSummary> InstallmentSummaries = new InstallmentDataAccess().GetInstallmentSummariesByBillAccountId(billAccount.BillAccountId);
            InstallmentSummary OldSummary = InstallmentSummaries.FirstOrDefault(summary => summary.PolicyNumber == newPayPlan.PolicyNumber);
            List<Installment> PendingInstallments = new InstallmentDataAccess().GetPendingInstallmentsBySummaryId(OldSummary.InstallmentSummaryId);
            double updatedPremium = 0.0;
            foreach(var pendingInstallment in PendingInstallments)
            {
                updatedPremium += (double)pendingInstallment.BalanceAmount;
            }
            return updatedPremium;
        }

        private InstallmentSummary CreateNewInstallmentSummary(BillAccount billAccount, BillAccountPolicy newPayPlan)
        {
            return new InstallmentSummary
            {
                PolicyNumber = newPayPlan.PolicyNumber,
                Status = ApplicationConstants.INSTALLMENT_SUMMARY_ACTIVE_STATUS,
                BillAccountId = billAccount.BillAccountId,
            };
        }

        private void CopyPaidInstallmentsToNewSchedule(BillAccount billAccount, BillAccountPolicy newPayPlan, InstallmentSummary newInstallmentSummary)
        {
            // Retrieve paid installments from the old schedule
            List<InstallmentSummary> InstallmentSummaries= new InstallmentDataAccess().GetInstallmentSummariesByBillAccountId(billAccount.BillAccountId);
            InstallmentSummary OldSummary = InstallmentSummaries.FirstOrDefault(summary => summary.PolicyNumber == newPayPlan.PolicyNumber);

            List<Installment> billedInstallments = new InstallmentDataAccess().GetBilledInstallmentsBySummaryId(OldSummary.InstallmentSummaryId);

            // Copy paid installments to the new schedule
            foreach (var billedInstallment in billedInstallments)
            {
                Installment newInstallment = new Installment
                {
                    InstallmentSequenceNumber = billedInstallment.InstallmentSequenceNumber,
                    InstallmentSendDate = billedInstallment.InstallmentSendDate,
                    InstallmentDueDate = billedInstallment.InstallmentDueDate,
                    DueAmount = billedInstallment.DueAmount,
                    PaidAmount = billedInstallment.PaidAmount,
                    BalanceAmount = billedInstallment.BalanceAmount,
                    InvoiceStatus = billedInstallment.InvoiceStatus,
                    InstallmentSummaryId = newInstallmentSummary.InstallmentSummaryId
                };
                new InstallmentDataAccess().AddInstallment(newInstallment);
            }
        }

        private void AddRemainingInstallmentsToNewSchedule(InstallmentSummary newInstallmentSummary, BillAccountPolicy newPayPlan, double premium, int dueDay)
        {
            // Generate and add remaining installments based on the new pay plan
            List<Installment> remainingInstallments = new InstallmentBusiness().GenerateInstallments(newInstallmentSummary, newPayPlan.PayPlan, premium, dueDay);
            foreach (var installment in remainingInstallments)
            {
                new InstallmentDataAccess().AddInstallment(installment);
            }
        }

        private void MarkPreviousScheduleAsExpired(BillAccount billAccount,BillAccountPolicy newPayPlan)
        {
            // Update the status of the previous installment summary to 'Expired'
            List<InstallmentSummary> InstallmentSummaries = new InstallmentDataAccess().GetInstallmentSummariesByBillAccountId(billAccount.BillAccountId);
            InstallmentSummary OldSummary = InstallmentSummaries.FirstOrDefault(summary => summary.PolicyNumber == newPayPlan.PolicyNumber);
           
                OldSummary.Status =ApplicationConstants.INSTALLMENT_SUMMARY_INACTIVE_STATUS;
                new InstallmentDataAccess().UpdateInstallmentSummary(OldSummary);
            
        }
    }
}

