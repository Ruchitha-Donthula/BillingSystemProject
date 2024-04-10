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
            // Arrange
            InstallmentBusiness installmentBusiness = new InstallmentBusiness();
            var newBillAccount = new BillAccount
            {

                BillingType = "quarterly",
                Status = "Active",
                PayorName = "ravaliDonthula",
                PayorAddress = "Hanamkonda",
                PaymentMethod = "Credit Card",
                DueDay = 15,
                AccountTotal = 1000.0,
                AccountPaid = 500.0,
                AccountBalance = 500.0,
                LastPaymentDate = DateTime.Now.AddDays(-30),
                LastPaymentAmount = 500.0,
                PastDue = 0.0,
                FutureDue = 0.0
            };

            // Add the new BillAccount
            new BillAccountBusiness().CreateBillAccount(newBillAccount);

            string policy = "Policy1238989";
            string payPlan = "quarterly";
            double premium = 60000.00;

            // Act
            installmentBusiness.CreateInstallmentSchedule(newBillAccount, policy, payPlan, premium);

            // Assert
            // You can add assertions here to verify the expected behavior, such as checking if records are properly saved to the database
        }

        /*
        public void TestInstallmentsInSummary()
        {
            new InstallmentBusiness().PrintInstallmentsInSummary(20);
             
        }*/
    }
}
