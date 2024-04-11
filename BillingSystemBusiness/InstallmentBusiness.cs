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
        public void CreateInstallmentSchedule(BillAccount billAccount, BillAccountPolicy billAccountPolicy, double premium)
        {
            ValidateInputParameters(billAccount, billAccountPolicy, premium);
            InstallmentSummary parentRecord = new InstallmentSummary
            {
                PolicyNumber = billAccountPolicy.PolicyNumber,
                Status = "Active",
                BillAccountId = billAccount.BillAccountId
            };
            new InstallmentSummaryDataAccess().AddInstallmentSummary(parentRecord);
            List<Installment> installments = GenerateInstallments(parentRecord, billAccountPolicy.PayPlan, premium, billAccount.DueDay);
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
                case "Monthly":
                    double monthlyPremium = premium / 12;
                    for (int installmentNumber = 1; installmentNumber <= 12; installmentNumber++)
                    {
                        installment = CreateInstallment(parentRecord, installmentNumber, monthlyPremium, payPlan, dueDay);
                        installments.Add(installment);
                    }
                    break;
                case "Quarterly":
                    double quarterlyPremium = premium / 4;
                    for (int installmentNumber = 1; installmentNumber <= 4; installmentNumber++)
                    {
                        installment = CreateInstallment(parentRecord, installmentNumber, quarterlyPremium, payPlan, dueDay);
                        installments.Add(installment);
                    }
                    break;
                case "Semiannual":
                    double semiannualPremium = premium / 2;
                    for (int installmentNumber = 1; installmentNumber <= 2; installmentNumber++)
                    {
                        installment = CreateInstallment(parentRecord, installmentNumber, semiannualPremium, payPlan, dueDay);
                        installments.Add(installment);
                    }
                    break;
                case "Annual":
                    double annualPremium = premium;
                    installment = CreateInstallment(parentRecord, 1, annualPremium, payPlan, dueDay);
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
                DueAmount = 0.0,
                PaidAmount = null,
                BalanceAmount = dueAmount,
                InvoiceStatus = "Pending",
                InstallmentSummaryId = parentRecord.InstallmentSummaryId,
            };
        }

        private DateTime CalculateSendDate(DateTime installmentDueDate)
        {
            return installmentDueDate.AddDays(-10);
        }

        private DateTime CalculateDueDate(int installmentNumber, string payPlan, int dueDay)
        {
            DateTime dueDate = DateTime.MinValue;
            switch (payPlan)
            {
                case "Monthly":
                    dueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddMonths(installmentNumber - 1);
                    break;
                case "Quarterly":
                    dueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddMonths((installmentNumber - 1) * 3);
                    break;
                case "Semiannual":
                    dueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddMonths((installmentNumber - 1) * 6);
                    break;
                case "Annual":
                    dueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dueDay).AddYears(installmentNumber - 1);
                    break;
            }
            return dueDate;
        }

        private void ValidateInputParameters(BillAccount billAccount, BillAccountPolicy billAccountPolicy, double premium)
        {
            if (billAccount == null)
            {
                throw new ArgumentNullException(nameof(billAccount), "BillAccount cannot be null");
            }
            if (billAccountPolicy == null)
            {
                throw new ArgumentNullException(nameof(billAccountPolicy), "PolicyNumber cannot be null");
            }
            if (premium <= 0)
            {
                throw new ArgumentNullException(nameof(premium), "premium cannot be null");
            }
        }
    }
}
