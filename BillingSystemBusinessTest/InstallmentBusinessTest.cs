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
                BillAccountId = 16
            };

            BillAccountPolicy billAccountPolicy = new BillAccountPolicy
            {
                BillAccountPolicyId=17,
                BillAccountId = 15,
                PayPlan = "Semiannual",
                PolicyNumber = "POL124"
            };
            double premium = 1000.00;

           new InstallmentBusiness().CreateInstallmentSchedule(billAccount, billAccountPolicy, premium);
            Console.WriteLine("Installments added successfully.");
        }
    }
}
