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
                BillAccountId = 1017
            };

            BillAccountPolicy billAccountPolicy = new BillAccountPolicy
            {
                BillAccountPolicyId=1013,
                BillAccountId = 1017,
                PayPlan = ApplicationConstants.POLICY_PAYPLAN_MONTHLY,
                PolicyNumber = "POL129"
            };
            double premium = 2400.00;

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
            new InstallmentRescheduling().OnChangeOfBillAccountDueDay(billAccount, DueDay);
        }
    }
}
