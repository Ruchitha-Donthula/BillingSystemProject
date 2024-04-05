using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class InvoiceInstallmentDataAccessTest
    {
        public void TestGetInvoiceInstallmentById()
        {
            Console.WriteLine("Testing GetInvoiceInstallmentById:");

            // Assuming there's an InvoiceInstallment with Id = 1 in the database
            var invoiceInstallment = new InvoiceInstallmentDataAccess().GetInvoiceInstallmentById(1);

            if (invoiceInstallment != null)
            {
                Console.WriteLine($"InvoiceInstallment found: Id = {invoiceInstallment.InvoiceInstallmentId}, InvoiceId = {invoiceInstallment.InvoiceId}, InstallmentId = {invoiceInstallment.InstallmentId}");
            }
            else
            {
                Console.WriteLine("InvoiceInstallment not found.");
            }
        }

       public void TestGetAllInvoiceInstallments()
        {
            Console.WriteLine("\nTesting GetAllInvoiceInstallments:");

            var invoiceInstallments = new InvoiceInstallmentDataAccess().GetAllInvoiceInstallments();

            if (invoiceInstallments.Count > 0)
            {
                Console.WriteLine("InvoiceInstallments found:");
                foreach (var invoiceInstallment in invoiceInstallments)
                {
                    Console.WriteLine($"Id = {invoiceInstallment.InvoiceInstallmentId}, InvoiceId = {invoiceInstallment.InvoiceId}, InstallmentId = {invoiceInstallment.InstallmentId}");
                }
            }
            else
            {
                Console.WriteLine("No InvoiceInstallments found.");
            }
        }

       public void TestAddInvoiceInstallment()
        {
            Console.WriteLine("\nTesting AddInvoiceInstallment:");

            // Create a new InvoiceInstallment object
            var newInvoiceInstallment = new InvoiceInstallment
            {
                InvoiceId = 562, // Assuming InvoiceId exists in the database
                InstallmentId = 172 // Assuming InstallmentId exists in the database
                // Add other properties as needed
            };

            // Add the new InvoiceInstallment
            new InvoiceInstallmentDataAccess().AddInvoiceInstallment(newInvoiceInstallment);
            Console.WriteLine("InvoiceInstallment added successfully.");
        }

       public  void TestDeleteInvoiceInstallment()
        {
            Console.WriteLine("\nTesting DeleteInvoiceInstallment:");

            // Assuming there's an InvoiceInstallment with Id = 1 in the database
            new InvoiceInstallmentDataAccess().DeleteInvoiceInstallment(1);
            Console.WriteLine("InvoiceInstallment deleted successfully.");
        }
    }
}
