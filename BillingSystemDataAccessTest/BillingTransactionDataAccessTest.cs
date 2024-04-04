using System;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class BillingTransactionDataAccessTest
    {

        public void TestGetBillingTransactionById(BillingTransactionDataAccess billingTransactionDataAccess)
        {
            Console.WriteLine("Testing GetBillingTransactionById:");

            // Assuming there's a BillingTransaction with Id = 1 in the database
            var billingTransaction = billingTransactionDataAccess.GetBillingTransactionById(1);

            if (billingTransaction != null)
            {
                Console.WriteLine($"BillingTransaction found: Id = {billingTransaction.BillingTransactionId}, ActivityDate = {billingTransaction.ActivityDate}");
            }
            else
            {
                Console.WriteLine("BillingTransaction not found.");
            }
        }

        public void TestGetAllBillingTransactions(BillingTransactionDataAccess billingTransactionDataAccess)
        {
            Console.WriteLine("\nTesting GetAllBillingTransactions:");

            var billingTransactions = billingTransactionDataAccess.GetAllBillingTransactions();

            if (billingTransactions.Count > 0)
            {
                Console.WriteLine("BillingTransactions found:");
                foreach (var billingTransaction in billingTransactions)
                {
                    Console.WriteLine($"Id = {billingTransaction.BillingTransactionId}, ActivityDate = {billingTransaction.ActivityDate}");
                }
            }
            else
            {
                Console.WriteLine("No BillingTransactions found.");
            }
        }

       public void TestAddBillingTransaction(BillingTransactionDataAccess billingTransactionDataAccess)
        {
            Console.WriteLine("\nTesting AddBillingTransaction:");

            // Create a new BillingTransaction object
            var newBillingTransaction = new BillingTransaction
            {
                ActivityDate = DateTime.Now,
                TransactionType = "TypeA",
                TransactionAmount = 100.0,
                BillAccountId = 3 // Assuming BillAccountId exists in the database
                // Add other properties as needed
            };

            // Add the new BillingTransaction
            billingTransactionDataAccess.AddBillingTransaction(newBillingTransaction);
            Console.WriteLine("BillingTransaction added successfully.");
        }

        public void TestDeleteBillingTransaction(BillingTransactionDataAccess billingTransactionDataAccess)
        {
            Console.WriteLine("\nTesting DeleteBillingTransaction:");

            // Assuming there's a BillingTransaction with Id = 1 in the database
            billingTransactionDataAccess.DeleteBillingTransaction(1);
            Console.WriteLine("BillingTransaction deleted successfully.");
        }
    }
}
