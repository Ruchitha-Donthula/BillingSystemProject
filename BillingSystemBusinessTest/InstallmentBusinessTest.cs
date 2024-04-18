using BillingSystemBusiness;
using BillingSystemDataModel;
using System;

namespace BillingSystemBusinessTest
{
    class InstallmentBusinessTest
    {
        public void CreateInstallmentSchedule()
        {
            var billAccount = new BillAccount
            {
                BillAccountId = 26
            };

            BillAccountPolicy billAccountPolicy = new BillAccountPolicy
            {
                BillAccountPolicyId=116,
                BillAccountId = 26,
                PayPlan = ApplicationConstants.POLICY_PAYPLAN_MONTHLY,
                PolicyNumber = "POL123"
            };
            double premium = 1200.00;

           new InstallmentBusiness().CreateInstallmentSchedule(billAccount, billAccountPolicy, premium);
            Console.WriteLine("Installments added successfully.");
        }
        public void TestInstallmentRescheduling()
        {
            var billAccount = new BillAccount
            {
                BillAccountId = 1017
            };
            int DueDay = 15;
            new InstallmentReschedulingBusiness().OnChangeOfBillAccountDueDay(billAccount, DueDay);
        }

        public void TestOnChangeOfPayPlan()
        {
            var billAccount = new BillAccount
            {
                BillAccountId = 26,
                DueDay=21,
            };

            BillAccountPolicy billAccountPolicy = new BillAccountPolicy
            {
                BillAccountPolicyId = 16,
                BillAccountId = 26,
                PayPlan = ApplicationConstants.POLICY_PAYPLAN_QUARTERLY,
                PolicyNumber = "POL123"
            };
            new InstallmentReschedulingBusiness().OnChangeOfPolicyPayPlan(billAccount, billAccountPolicy);
        }
        
    }
}
