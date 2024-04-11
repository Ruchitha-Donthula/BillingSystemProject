using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class InvoiceInstallmentDataAccess
    {
        private readonly BillingSystemEDMContainer _context;

        public InvoiceInstallmentDataAccess()
        {
            _context = new BillingSystemEDMContainer(); 
        }

        public InvoiceInstallment GetInvoiceInstallmentById(int id)
        {
            try
            {
                return _context.InvoiceInstallments.FirstOrDefault(i => i.InvoiceInstallmentId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while retrieving InvoiceInstallment by Id: " + ex.Message);
                return null;
            }
        }

        public List<InvoiceInstallment> GetAllInvoiceInstallments()
        {
            try
            {
                return _context.InvoiceInstallments.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while retrieving all InvoiceInstallments: " + ex.Message);
                return new List<InvoiceInstallment>();
            }
        }

        public void AddInvoiceInstallment(InvoiceInstallment invoiceInstallment)
        {
            try
            {
                _context.InvoiceInstallments.Add(invoiceInstallment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while adding InvoiceInstallment: " + ex.Message);
            }
        }

        public void DeleteInvoiceInstallment(int id)
        {
            try
            {
                var invoiceInstallment = _context.InvoiceInstallments.Find(id);
                if (invoiceInstallment != null)
                {
                    _context.InvoiceInstallments.Remove(invoiceInstallment);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while deleting InvoiceInstallment: " + ex.Message);
            }
        }
    }
}
