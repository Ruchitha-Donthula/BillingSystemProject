using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class BillAccountDataAccessTest
    { 
       public void TestAddBillAccount(BillAccountDataAccess billAccountDataAccess)
        {
            Console.WriteLine("Testing AddBillAccount:");

            // Create a new BillAccount object
            var newBillAccount = new BillAccount
            {
                BillAccountNumber = "BA123456",
                BillingType = "Monthly",
                Status = "Active",
                PayorName = "John Doe",
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
            billAccountDataAccess.AddBillAccount(newBillAccount);
            Console.WriteLine("BillAccount added successfully.");
        }

        public void TestUpdateBillAccount(BillAccountDataAccess billAccountDataAccess)
        {
            Console.WriteLine("\nTesting UpdateBillAccount:");

            // Get an existing BillAccount by Id
            var billAccount = billAccountDataAccess.GetBillAccountByNumber("BA123457");

            if (billAccount != null)
            {
                // Update BillAccount properties
                billAccount.BillingType = "Yearly";

                // Update the BillAccount
                billAccountDataAccess.UpdateBillAccount(billAccount);
                Console.WriteLine("BillAccount updated successfully.");
            }
            else
            {
                Console.WriteLine("BillAccount not found.");
            }
        }

        public void TestDeleteBillAccount(BillAccountDataAccess billAccountDataAccess)
        {
            Console.WriteLine("\nTesting DeleteBillAccount:");

            // Assuming there's a BillAccount with Id = 2 in the database
            billAccountDataAccess.DeleteBillAccount(2);
            Console.WriteLine("BillAccount deleted successfully.");
        }

        public void TestGetBillAccountById(BillAccountDataAccess billAccountDataAccess)
        {
            Console.WriteLine("\nTesting GetBillAccountById:");

            // Assuming there's a BillAccount with Id = 2 in the database
            var billAccount = billAccountDataAccess.GetBillAccountById(4);

            if (billAccount != null)
            {
                Console.WriteLine($"BillAccount found: Id = {billAccount.BillAccountId}, BillAccountNumber = {billAccount.BillAccountNumber}");
            }
            else
            {
                Console.WriteLine("BillAccount not found.");
            }
        }

        public void TestGetBillAccountByNumber(BillAccountDataAccess billAccountDataAccess)
        {
            Console.WriteLine("\nTesting GetBillAccountByNumber:");

            // Assuming there's a BillAccount with Id = 2 in the database
            var billAccount = billAccountDataAccess.GetBillAccountByNumber("BA123457");

            if (billAccount != null)
            {
                Console.WriteLine($"BillAccount found: Id = {billAccount.BillAccountId}, BillAccountNumber = {billAccount.BillAccountNumber}");
            }
            else
            {
                Console.WriteLine("BillAccount not found.");
            }
        }

        public void TestGetAllBillAccounts(BillAccountDataAccess billAccountDataAccess)
        {
            Console.WriteLine("\nTesting GetAllBillAccounts:");

            var billAccounts = billAccountDataAccess.GetAllBillAccounts();

            if (billAccounts.Count > 0)
            {
                Console.WriteLine("BillAccounts found:");
                foreach (var billAccount in billAccounts)
                {
                    Console.WriteLine($"Id = {billAccount.BillAccountId}, BillAccountNumber = {billAccount.BillAccountNumber}");
                }
            }
            else
            {
                Console.WriteLine("No BillAccounts found.");
            }
        }

        public void TestSuspendBillAccount()
        {
            var billAccount = new BillAccount
            {
                BillAccountId=5,
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
            new BillAccountDataAccess().SuspendBillAccount(billAccount);
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
            new BillAccountDataAccess().SuspendBillAccount(billAccount);
            Console.WriteLine("BillAccount suspended Successfully");
        }
    }
}
