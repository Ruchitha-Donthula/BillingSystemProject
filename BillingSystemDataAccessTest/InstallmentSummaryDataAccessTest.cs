using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class InstallmentSummaryDataAccessTest
    {
        public void TestGetInstallmentSummaryById(InstallmentSummaryDataAccess installmentSummaryDataAccess)
        {
            Console.WriteLine("Testing GetInstallmentSummaryById:");

            // Assuming there's an InstallmentSummary with Id = 1 in the database
            var installmentSummary = installmentSummaryDataAccess.GetInstallmentSummaryById(1);

            if (installmentSummary != null)
            {
                Console.WriteLine($"InstallmentSummary found: Id = {installmentSummary.InstallmentSummaryId}, PolicyNumber = {installmentSummary.PolicyNumber}");
            }
            else
            {
                Console.WriteLine("InstallmentSummary not found.");
            }
        }

        public void TestGetAllInstallmentSummaries(InstallmentSummaryDataAccess installmentSummaryDataAccess)
        {
            Console.WriteLine("\nTesting GetAllInstallmentSummaries:");

            var installmentSummaries = installmentSummaryDataAccess.GetAllInstallmentSummaries();

            if (installmentSummaries.Count > 0)
            {
                Console.WriteLine("InstallmentSummaries found:");
                foreach (var installmentSummary in installmentSummaries)
                {
                    Console.WriteLine($"Id = {installmentSummary.InstallmentSummaryId}, PolicyNumber = {installmentSummary.PolicyNumber}");
                }
            }
            else
            {
                Console.WriteLine("No InstallmentSummaries found.");
            }
        }

        public void TestAddInstallmentSummary(InstallmentSummaryDataAccess installmentSummaryDataAccess)
        {
            Console.WriteLine("\nTesting AddInstallmentSummary:");

            // Create a new InstallmentSummary object
            var newInstallmentSummary = new InstallmentSummary
            {
                BillAccountId = 3,
                PolicyNumber = "POL123456",
                Status = "Active",
               // Assuming BillAccountId exists in the database
                // Add other properties as needed
            };

            // Add the new InstallmentSummary
            installmentSummaryDataAccess.AddInstallmentSummary(newInstallmentSummary);
            Console.WriteLine("InstallmentSummary added successfully.");
        }

        public void TestUpdateInstallmentSummary(InstallmentSummaryDataAccess installmentSummaryDataAccess)
        {
            Console.WriteLine("\nTesting UpdateInstallmentSummary:");

            // Get an existing InstallmentSummary by Id
            var installmentSummary = installmentSummaryDataAccess.GetInstallmentSummaryById(1);

            if (installmentSummary != null)
            {
                // Update InstallmentSummary properties
                installmentSummary.Status = "Inactive";

                // Update the InstallmentSummary
                installmentSummaryDataAccess.UpdateInstallmentSummary(installmentSummary);
                Console.WriteLine("InstallmentSummary updated successfully.");
            }
            else
            {
                Console.WriteLine("InstallmentSummary not found.");
            }
        }

       public  void TestDeleteInstallmentSummary(InstallmentSummaryDataAccess installmentSummaryDataAccess)
        {
            Console.WriteLine("\nTesting DeleteInstallmentSummary:");

            // Assuming there's an InstallmentSummary with Id = 1 in the database
            installmentSummaryDataAccess.DeleteInstallmentSummary(1);
            Console.WriteLine("InstallmentSummary deleted successfully.");
        }
    }
}