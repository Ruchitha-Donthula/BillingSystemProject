using System;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class BillAccountPolicyDataAccessTest
    {
       
       public void TestGetBillAccountPolicyById()
        {
            Console.WriteLine("Testing GetBillAccountPolicyById:");

            // Assuming there's a BillAccountPolicy with Id = 2 in the database
            var billAccountPolicy = new BillAccountPolicyDataAccess().GetBillAccountPolicyById(2);

            if (billAccountPolicy != null)
            {
                Console.WriteLine($"BillAccountPolicy found: Id = {billAccountPolicy.BillAccountPolicyId}, PolicyNumber = {billAccountPolicy.PolicyNumber}");
            }
            else
            {
                Console.WriteLine("BillAccountPolicy not found.");
            }
        }

       public void TestGetAllBillAccountPolicies()
        {
            Console.WriteLine("\nTesting GetAllBillAccountPolicies:");

            var billAccountPolicies = new BillAccountPolicyDataAccess().GetAllBillAccountPolicies();

            if (billAccountPolicies.Count > 0)
            {
                Console.WriteLine("BillAccountPolicies found:");
                foreach (var billAccountPolicy in billAccountPolicies)
                {
                    Console.WriteLine($"Id = {billAccountPolicy.BillAccountPolicyId}, PolicyNumber = {billAccountPolicy.PolicyNumber}");
                }
            }
            else
            {
                Console.WriteLine("No BillAccountPolicies found.");
            }
        }

       public void TestAddBillAccountPolicy()
        {
            Console.WriteLine("\nTesting AddBillAccountPolicy:");

            // Create a new BillAccountPolicy object
            var newBillAccountPolicy = new BillAccountPolicy
            {
                PolicyNumber = "POL123456",
                BillAccountId = 3 // Assuming BillAccountId exists in the database
                // Add other properties as needed
            };

            // Add the new BillAccountPolicy
            new BillAccountPolicyDataAccess().AddBillAccountPolicy(newBillAccountPolicy);
            Console.WriteLine("BillAccountPolicy added successfully.");
        }

       public void TestDeleteBillAccountPolicy()
        {
            Console.WriteLine("\nTesting DeleteBillAccountPolicy:");

            // Assuming there's a BillAccountPolicy with Id = 2 in the database
           new BillAccountPolicyDataAccess().DeleteBillAccountPolicy(2);
            Console.WriteLine("BillAccountPolicy deleted successfully.");
        }
    }
}
