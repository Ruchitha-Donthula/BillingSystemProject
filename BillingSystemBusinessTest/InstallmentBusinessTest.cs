using System;
using BillingSystemBusiness;
using BillingSystemDataModel;
using BillingSystemBusiness;

namespace BillingSystemBusinessTest
{
    class InstallmentBusinessTest
    {
        public void CreateInstallmentSchedules()
        {
            InstallmentBusiness installmentBusiness = new InstallmentBusiness();
            var billAccount = new BillAccount
            {
                BillAccountId = 8,
                DueDay=15,
            };

            BillAccountPolicy billAccountPolicy = new BillAccountPolicy
            {
                BillAccountId = billAccount.BillAccountId,
                PayPlan = "Monthly",
                PolicyNumber = "POL123"
            };
            double premium = 1200.00;

           new InstallmentBusiness().CreateInstallmentSchedule(billAccount, billAccountPolicy, premium);
        }

        /*
        public void TestInstallmentsInSummary()
        {
            new InstallmentBusiness().PrintInstallmentsInSummary(20);
             
        }*/
    }
}
