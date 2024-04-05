using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataModel;
using BillingSystemBusiness;

namespace BillingSystemBusinessTest
{
    class BillAccountBusinessTest
    {
        public static void Main(string[] args)
        {
            //new BillAccountBusinessTest().TestCreateBillAccount();
            //new BillAccountBusinessTest().TestBillAccountPolicy();
            // new BillAccountBusinessTest().TestGetBillAccountById();
            //new BillAccountBusinessTest().TestGetBillAccountByNumber();
            //new BillAccountBusinessTest().TestUpdateBillAccount();
            //new BillAccountBusinessTest().TestSuspendBillAccount();
            new BillAccountBusinessTest().TestReleaseBillAccount();
        }
        public void TestCreateBillAccount()
        {
            Console.WriteLine("Testing AddBillAccount:");

            // Create a new BillAccount object
            var newBillAccount = new BillAccount
            {
                BillingType = "Monthly",
                Status = "Active",
                PayorName = "Mahalaxmi",
                PayorAddress = "123 Main Street",
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
            Console.WriteLine("BillAccount added successfully.");
        }
        public void TestBillAccountPolicy()
        {
            var billAccount = new BillAccount
            {
                BillAccountId=5,
                BillAccountNumber= "BA123457",
                BillingType = "Monthly",
                Status = "Active",
                PayorName = "Mahalaxmi",
                PayorAddress = "123 Main Street",
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
            List<string> policyNumbers = new List<string> { "Policy123", "Policy456" };
            new BillAccountBusiness().AssociateBillAccountWithPolicy(billAccount, policyNumbers);
            Console.WriteLine("BillAccountPolicies added successfully.");

        }

        public void TestGetBillAccountById()
        {
            BillAccount billAccount = new BillAccountBusiness().GetBillAccountById(6);
            Console.WriteLine(billAccount.BillAccountId + " " + billAccount.BillAccountNumber + " " + billAccount.PayorName);
        }

        public void TestGetBillAccountByNumber()
        {
            BillAccount billAccount = new BillAccountBusiness().GetBillAccountByNumber("BA123457");
            Console.WriteLine(billAccount.BillAccountId + " " + billAccount.BillAccountNumber + " " + billAccount.PayorName);
        }
        public void TestUpdateBillAccount()
        {
            var billAccount = new BillAccount
            {
                BillAccountNumber = "BA123457",
                BillingType = "Monthly",
                Status = "Active",
                PayorName = "MahalaxmiGouda",
                PayorAddress = "123 Main Street,Apollopharmacy",
                PaymentMethod = "CreditCard",
                DueDay = 5,
                AccountTotal = 1900.0,
                AccountPaid = 50.0,
                AccountBalance = 1850.0,
                LastPaymentDate = DateTime.Now.AddDays(-30),
                LastPaymentAmount = 500.0,
                PastDue = 0.0,
                FutureDue = 0.0
            };
            new BillAccountBusiness().UpdateBillAccount(billAccount);
            Console.WriteLine("BillAccount updated Successfully");
        }
        public void TestSuspendBillAccount()
        {
            var billAccount = new BillAccount
            {
                BillAccountId = 5,
                BillAccountNumber = "BA123457",
                BillingType = "Monthly",
                Status = "Active",
                PayorName = "MahalaxmiGouda",
                PayorAddress = "123 Main Street,Apollopharmacy",
                PaymentMethod = "CreditCard",
                DueDay = 5,
                AccountTotal = 1900.0,
                AccountPaid = 50.0,
                AccountBalance = 1850.0,
                LastPaymentDate = DateTime.Now.AddDays(-30),
                LastPaymentAmount = 500.0,
                PastDue = 0.0,
                FutureDue = 0.0
            };
            new BillAccountBusiness().SuspendBillAccount(billAccount);
            Console.WriteLine("BillAccount suspended Successfully");
        }
        public void TestReleaseBillAccount()
        {
            var billAccount = new BillAccount
            {
                BillAccountId = 5,
                BillAccountNumber = "BA123457",
                BillingType = "Monthly",
                Status = "Active",
                PayorName = "MahalaxmiGouda",
                PayorAddress = "123 Main Street,Apollopharmacy",
                PaymentMethod = "CreditCard",
                DueDay = 5,
                AccountTotal = 1900.0,
                AccountPaid = 50.0,
                AccountBalance = 1850.0,
                LastPaymentDate = DateTime.Now.AddDays(-30),
                LastPaymentAmount = 500.0,
                PastDue = 0.0,
                FutureDue = 0.0
            };
            new BillAccountBusiness().ReleaseBillAccount(billAccount);
            Console.WriteLine("BillAccount released Successfully");
        }

    }
}
