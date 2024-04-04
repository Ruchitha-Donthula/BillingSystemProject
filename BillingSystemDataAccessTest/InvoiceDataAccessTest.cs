using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class InvoiceDataAccessTest
    {
       public  void TestGetInvoiceById(InvoiceDataAccess invoiceDataAccess)
        {
            Console.WriteLine("Testing GetInvoiceById:");

            // Assuming there's an Invoice with Id = 1 in the database
            var invoice = invoiceDataAccess.GetInvoiceById(1);

            if (invoice != null)
            {
                Console.WriteLine($"Invoice found: Id = {invoice.InvoiceId}, InvoiceNumber = {invoice.InvoiceNumber}");
            }
            else
            {
                Console.WriteLine("Invoice not found.");
            }
        }

        public void TestGetAllInvoices(InvoiceDataAccess invoiceDataAccess)
        {
            Console.WriteLine("\nTesting GetAllInvoices:");

            var invoices = invoiceDataAccess.GetAllInvoices();

            if (invoices.Count > 0)
            {
                Console.WriteLine("Invoices found:");
                foreach (var invoice in invoices)
                {
                    Console.WriteLine($"Id = {invoice.InvoiceId}, InvoiceNumber = {invoice.InvoiceNumber}");
                }
            }
            else
            {
                Console.WriteLine("No Invoices found.");
            }
        }

       public void TestAddInvoice(InvoiceDataAccess invoiceDataAccess)
        {
            Console.WriteLine("\nTesting AddInvoice:");

            // Create a new Invoice object
            var newInvoice = new Invoice
            {
                InvoiceNumber = "INV123456",
                InvoiceDate = DateTime.Now,
                SendDate = DateTime.Now.AddDays(1),
                BillAccountId = 2, // Assuming BillAccountId exists in the database
                Status = "Pending",
                InvoiceAmount = 1000.0
                // Add other properties as needed
            };

            // Add the new Invoice
            invoiceDataAccess.AddInvoice(newInvoice);
            Console.WriteLine("Invoice added successfully.");
        }

       public void TestUpdateInvoice(InvoiceDataAccess invoiceDataAccess)
        {
            Console.WriteLine("\nTesting UpdateInvoice:");

            // Get an existing Invoice by Id
            var invoice = invoiceDataAccess.GetInvoiceById(1);

            if (invoice != null)
            {
                // Update Invoice properties
                invoice.Status = "Paid";

                // Update the Invoice
                invoiceDataAccess.UpdateInvoice(invoice);
                Console.WriteLine("Invoice updated successfully.");
            }
            else
            {
                Console.WriteLine("Invoice not found.");
            }
        }

       public void TestDeleteInvoice(InvoiceDataAccess invoiceDataAccess)
        {
            Console.WriteLine("\nTesting DeleteInvoice:");

            // Assuming there's an Invoice with Id = 1 in the database
            invoiceDataAccess.DeleteInvoice(1);
            Console.WriteLine("Invoice deleted successfully.");
        }
    }
}
