using System;
using System.Collections.Generic;
using BillingSystemDataModel;
using BillingSystemDataAccess;

namespace BillingSystemBusiness
{
    public class InstallmentBusiness
    {
        public void CreateInstallmentSchedule(BillAccount BillAccount, BillAccountPolicy billAccountPolicy, double premium)
        {
            try
            {
                BillAccount billAccount = new BillAccountDataAccess().GetBillAccountById(BillAccount.BillAccountId);
                if (billAccount == null)
                    throw new Exception("Bill account not found.");

                InitializeBillAccount(billAccount, premium);

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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating installment schedule.", ex);
            }
        }

        private void InitializeBillAccount(BillAccount billAccount, double premium)
        {
            try
            {
                if (billAccount == null)
                    throw new ArgumentNullException(nameof(billAccount), "Bill account cannot be null.");

                double accountTotal = (double)(billAccount.AccountTotal + premium);
                billAccount.AccountTotal = accountTotal;
                billAccount.AccountBalance = billAccount.AccountTotal - billAccount.AccountPaid;
                new BillAccountDataAccess().UpdateBillAccount(billAccount);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while initializing bill account.", ex);
            }
        }

        private List<Installment> GenerateInstallments(InstallmentSummary parentRecord, string payPlan, double premium, int dueDay)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating installments.", ex);
            }
        }

        private Installment CreateInstallment(InstallmentSummary parentRecord, int installmentNumber, double dueAmount, string payPlan, int dueDay)
        {
            try
            {
                DateTime installmentDueDate = CalculateDueDate(installmentNumber, payPlan, dueDay);
                DateTime installmentSendDate = CalculateSendDate(installmentDueDate);
                return new Installment
                {
                    InstallmentSequenceNumber = installmentNumber,
                    InstallmentDueDate = installmentDueDate,
                    InstallmentSendDate = installmentSendDate,
                    DueAmount = dueAmount,
                    PaidAmount = 0.0,
                    BalanceAmount = dueAmount,
                    InvoiceStatus = "Pending",
                    InstallmentSummaryId = parentRecord.InstallmentSummaryId,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating installment.", ex);
            }
        }

        private DateTime CalculateSendDate(DateTime installmentDueDate)
        {
            return installmentDueDate.AddDays(-10);
        }

        private DateTime CalculateDueDate(int installmentNumber, string payPlan, int dueDay)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while calculating due date.", ex);
            }
        }
    }
}
