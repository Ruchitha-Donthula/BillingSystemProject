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
                BillAccountId = 1015
            };

            BillAccountPolicy billAccountPolicy = new BillAccountPolicy
            {
                BillAccountPolicyId=1011,
                BillAccountId = 1015,
                PayPlan = ApplicationConstants.POLICY_PAYPLAN_SEMIANNUAL,
                PolicyNumber = "POL127"
            };
            double premium = 1000.00;

           new InstallmentBusiness().CreateInstallmentSchedule(billAccount, billAccountPolicy, premium);
            Console.WriteLine("Installments added successfully.");
        }
    }
}
