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
                BillAccountId = 8,
                BillAccountNumber="BA000001",
                BillingType = "Direct",
                Status = "Active",
                PayorName = "Prakash",
                PayorAddress = "SubashNagar",
                PaymentMethod = "Credit Card",
                DueDay = 21,
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
