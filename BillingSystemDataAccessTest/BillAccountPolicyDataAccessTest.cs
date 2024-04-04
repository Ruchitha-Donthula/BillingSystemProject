using System;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class BillAccountPolicyDataAccessTest
    {
       
       public void TestGetBillAccountPolicyById(BillAccountPolicyDataAccess billAccountPolicyDataAccess)
        {
            Console.WriteLine("Testing GetBillAccountPolicyById:");

            // Assuming there's a BillAccountPolicy with Id = 2 in the database
            var billAccountPolicy = billAccountPolicyDataAccess.GetBillAccountPolicyById(2);

            if (billAccountPolicy != null)
            {
                Console.WriteLine($"BillAccountPolicy found: Id = {billAccountPolicy.BillAccountPolicyId}, PolicyNumber = {billAccountPolicy.PolicyNumber}");
            }
            else
            {
                Console.WriteLine("BillAccountPolicy not found.");
            }
        }

       public void TestGetAllBillAccountPolicies(BillAccountPolicyDataAccess billAccountPolicyDataAccess)
        {
            Console.WriteLine("\nTesting GetAllBillAccountPolicies:");

            var billAccountPolicies = billAccountPolicyDataAccess.GetAllBillAccountPolicies();

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

       public void TestAddBillAccountPolicy(BillAccountPolicyDataAccess billAccountPolicyDataAccess)
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
            billAccountPolicyDataAccess.AddBillAccountPolicy(newBillAccountPolicy);
            Console.WriteLine("BillAccountPolicy added successfully.");
        }

       public void TestDeleteBillAccountPolicy(BillAccountPolicyDataAccess billAccountPolicyDataAccess)
        {
            Console.WriteLine("\nTesting DeleteBillAccountPolicy:");

            // Assuming there's a BillAccountPolicy with Id = 2 in the database
            billAccountPolicyDataAccess.DeleteBillAccountPolicy(2);
            Console.WriteLine("BillAccountPolicy deleted successfully.");
        }
    }
}
