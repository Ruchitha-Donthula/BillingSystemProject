using System;
using System.Collections.Generic;
using System.Linq;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class BillingTransactionDataAccess
    {
        private readonly BillingSystemEDMContainer _context;

        public BillingTransactionDataAccess()
        {
            _context = new BillingSystemEDMContainer(); // Assuming you have a DbContext named BillingSystemEDMContainer
        }

        public BillingTransaction GetBillingTransactionById(int id)
        {
            try
            {
                return _context.BillingTransactions.FirstOrDefault(b => b.BillingTransactionId == id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while retrieving BillingTransaction by Id: " + ex.Message);
                return null;
            }
        }

        public List<BillingTransaction> GetAllBillingTransactions()
        {
            try
            {
                return _context.BillingTransactions.ToList();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while retrieving all BillingTransactions: " + ex.Message);
                return new List<BillingTransaction>();
            }
        }

        public void AddBillingTransaction(BillingTransaction billingTransaction)
        {
            try
            {
                _context.BillingTransactions.Add(billingTransaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while adding BillingTransaction: " + ex.Message);
            }
        }

        public void DeleteBillingTransaction(int id)
        {
            try
            {
                var billingTransaction = _context.BillingTransactions.Find(id);
                if (billingTransaction != null)
                {
                    _context.BillingTransactions.Remove(billingTransaction);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while deleting BillingTransaction: " + ex.Message);
            }
        }
    }
}
