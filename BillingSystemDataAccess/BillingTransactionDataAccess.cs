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
            _context = new BillingSystemEDMContainer();
        }

        public BillingTransaction GetBillingTransactionById(int id)
        {
            try
            {
                return _context.BillingTransactions.FirstOrDefault(b => b.BillingTransactionId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving BillingTransaction by Id.", ex);
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
                throw new Exception("An error occurred while retrieving all BillingTransactions.", ex);
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
                throw new Exception("An error occurred while adding BillingTransaction.", ex);
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
                throw new Exception("An error occurred while deleting BillingTransaction.", ex);
            }
        }
    }
}
