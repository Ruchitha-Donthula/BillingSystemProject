using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new Exception("An error occurred while retrieving Invoice by Id.", ex);
            }
        }

        public Invoice GetInvoiceByNumber(string number)
        {
            try
            {
                return _context.Invoices.FirstOrDefault(i => i.InvoiceNumber == number);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving Invoice by Number.", ex);
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
                throw new Exception("An error occurred while retrieving all Invoices.", ex);
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
                throw new Exception("An error occurred while adding Invoice.", ex);
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
                throw new Exception("An error occurred while updating Invoice.", ex);
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
                throw new Exception("An error occurred while deleting Invoice.", ex);
            }
        }

        public int GetNextSequenceNumber()
        {
            try
            {
                int nextSequenceNumber = 1; // Default if no records exist
                var latestInvoiceNumber = _context.Invoices.OrderByDescending(b => b.BillAccountId).FirstOrDefault();
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the next sequence number.", ex);
            }
        }
    }
}
