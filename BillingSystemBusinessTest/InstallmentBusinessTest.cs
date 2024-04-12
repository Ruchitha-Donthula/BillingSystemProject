using BillingSystemBusiness;
using BillingSystemDataModel;

namespace BillingSystemBusinessTest
{
    class InstallmentBusinessTest
    {
        public void CreateInstallmentSchedule()
        {
            var billAccount = new BillAccount
            {
                BillAccountId = 11,
                BillAccountNumber="BA000003",
                BillingType = "Agent",
                Status = "Active",
                PayorName = "Mahalaxmi",
                PayorAddress = "Knr",
                PaymentMethod = "Credit Card",
                DueDay = 23,
                AccountTotal = 0.0,
                AccountPaid = 0.0,
                AccountBalance = 0.0,
                LastPaymentDate = null,
                LastPaymentAmount = 0.0,
                PastDue = 0.0,
                FutureDue = 0.0
            };

            BillAccountPolicy billAccountPolicy = new BillAccountPolicy
            {
                BillAccountPolicyId=9,
                BillAccountId = 11,
                PayPlan = "Semiannual",
                PolicyNumber = "POL124"
            };
            double premium = 2000.00;

           new InstallmentBusiness().CreateInstallmentSchedule(billAccount, billAccountPolicy, premium);
        }

        /*
        public void TestInstallmentsInSummary()
        {
            new InstallmentBusiness().PrintInstallmentsInSummary(20);
             
        }*/
    }
}
