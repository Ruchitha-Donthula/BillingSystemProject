using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataModel;
using BillingSystemDataAccess;

namespace BillingSystemBusiness
{
    public class InstallmentBusiness
    {
        public void CreateInstallmentSchedule(BillAccount billAccount, string policyNumber, string payPlan, double premium)
        {
            ValidateInputParameters(billAccount, policyNumber, payPlan, premium);
            InstallmentSummary parentRecord = new InstallmentSummary
            {
                PolicyNumber = policyNumber,
                Status = "Active",
                BillAccountId = billAccount.BillAccountId
            };
            new InstallmentSummaryDataAccess().AddInstallmentSummary(parentRecord);
            List<Installment> installments = GenerateInstallments(parentRecord, payPlan, premium, billAccount.DueDay);
            foreach (var installment in installments)
            {
                new InstallmentDataAccess().AddInstallment(installment);
            }
        }


        private List<Installment> GenerateInstallments(InstallmentSummary parentRecord, string payPlan, double premium, int dueDay)
        {
            List<Installment> installments = new List<Installment>();
            Installment installment;
            switch (payPlan)
            {
                case "monthly":
                    double monthlyPremium = premium / 12;
                    for (int i = 1; i <= 12; i++)
                    {
                        installment = CreateInstallment(parentRecord, i, monthlyPremium, payPlan, dueDay);
                        installments.Add(installment);
                    }
                    break;
                case "quarterly":
                    double quarterlyPremium = premium / 4;
                    for (int i = 1; i <= 4; i++)
                    {
                        installment = CreateInstallment(parentRecord, i, quarterlyPremium, payPlan, dueDay);
                        installments.Add(installment);
                    }
                    break;
                case "semiannual":
                    double semiannualpremium = premium / 2;
                    for (int i = 1; i <= 2; i++)
                    {
                        installment = CreateInstallment(parentRecord, i, semiannualpremium, payPlan, dueDay);
                        installments.Add(installment);
                    }
                    break;
                case "annual":
                    double annualpremium = premium;
                    installment = CreateInstallment(parentRecord, 1, annualpremium, payPlan, dueDay);
                    installments.Add(installment);
                    break;
                default:
                    break;
            }

            return installments;
        }

        private Installment CreateInstallment(InstallmentSummary parentRecord, int installmentNumber, double dueAmount, string payPlan, int dueDay)
        {
            DateTime installmentDueDate = CalculateDueDate(installmentNumber, payPlan, dueDay);
            DateTime installmentSendDate = CalculateSendDate(installmentDueDate);
            return new Installment
            {
                InstallmentSequenceNumber = installmentNumber,
                InstallmentDueDate = installmentDueDate,
                InstallmentSendDate = installmentSendDate,
                DueAmount = dueAmount,
                PaidAmount = null,
                BalanceAmount = dueAmount,
                InvoiceStatus = "Pending",
                InstallmentSummaryId = parentRecord.InstallmentSummaryId,
            };
        }
        private DateTime CalculateSendDate(DateTime InstallmentDueDate)
        {
            return InstallmentDueDate.AddDays(-10);
        }

        private DateTime CalculateDueDate(int installmentNumber, string payPlan, int dueDay)
        {
            DateTime DueDate = DateTime.MinValue;
            switch (payPlan)
            {
                case "monthly":
                    DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddMonths(installmentNumber - 1);
                    break;
                case "quarterly":
                    DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddMonths((installmentNumber - 1) * 3);
                    break;
                case "semiannual":
                    DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddMonths((installmentNumber - 1) * 6);
                    break;
                case "annual":
                    DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddYears(installmentNumber - 1);
                    break;
            }
            return DueDate;
        }
        private void ValidateInputParameters(BillAccount billAccount, string policyNumber, string payPlan, double premium)
        {
            // Perform validation on input parameters
            if (billAccount == null)
            {
                throw new ArgumentNullException(nameof(billAccount), "BillAccount cannot be null");
            }
            if (policyNumber == null)
            {
                throw new ArgumentNullException(nameof(policyNumber), "PolicyNumber cannot be null");
            }
            if (payPlan == null)
            {
                throw new ArgumentNullException(nameof(payPlan), "payPlan cannot be null");
            }
            if (premium <= 0)
            {
                throw new ArgumentNullException(nameof(premium), "premium cannot be null");
            }
        }

    }

}