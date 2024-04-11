using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class InvoiceDataAccess
    {
        private readonly BillingSystemEDMContainer _context;

        public InvoiceDataAccess()
        {
            _context = new BillingSystemEDMContainer(); 
        }

        public Invoice GetInvoiceById(int id)
        {
            try
            {
                return _context.Invoices.FirstOrDefault(i => i.InvoiceId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while retrieving Invoice by Id: " + ex.Message);
                return null;
            }
        }

        public List<Invoice> GetAllInvoices()
        {
            try
            {
                return _context.Invoices.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while retrieving all Invoices: " + ex.Message);
                return new List<Invoice>();
            }
        }

        public void AddInvoice(Invoice invoice)
        {
            try
            {
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while adding Invoice: " + ex.Message);
            }
        }

        public void UpdateInvoice(Invoice invoice)
        {
            try
            {
                var existingInvoice = _context.Invoices.Find(invoice.InvoiceId);
                if (existingInvoice != null)
                {
                    existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
                    existingInvoice.InvoiceDate = invoice.InvoiceDate;
                    existingInvoice.SendDate = invoice.SendDate;
                    existingInvoice.BillAccountId = invoice.BillAccountId;
                    existingInvoice.Status = invoice.Status;
                    existingInvoice.InvoiceAmount = invoice.InvoiceAmount;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating Invoice: " + ex.Message);
            }
        }

        public void DeleteInvoice(int id)
        {
            try
            {
                var invoice = _context.Invoices.Find(id);
                if (invoice != null)
                {
                    _context.Invoices.Remove(invoice);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while deleting Invoice: " + ex.Message);
            }
        }

        public int GetNextSequenceNumber()
        {
            var _dbContext = new BillingSystemEDMContainer();
            int nextSequenceNumber = 1; // Default if no records exist
            var latestInvoiceNumber = _dbContext.Invoices.OrderByDescending(b => b.BillAccountId).FirstOrDefault();
            if (latestInvoiceNumber != null)
            {
                // Extract the numeric part and increment by 1
                string numericPart = latestInvoiceNumber.InvoiceNumber.Substring(2);
                if (int.TryParse(numericPart, out int numericValue))
                {
                    nextSequenceNumber = numericValue + 1;
                }
            }
            return nextSequenceNumber;
        }
    }
}
