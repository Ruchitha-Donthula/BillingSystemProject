using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class BillAccountDataAccess
    {
        private readonly BillingSystemEDMContainer _context;

        public BillAccountDataAccess()
        {
            _context = new BillingSystemEDMContainer(); // Assuming you have a DbContext named BillingSystemContext
        }

        public BillAccount GetBillAccountById(int id)
        {
            try
            {
                return _context.BillAccounts.FirstOrDefault(b => b.BillAccountId == id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while retrieving BillAccount by Id: " + ex.Message);
                return null;
            }
        }

        public List<BillAccount> GetAllBillAccounts()
        {
            try
            {
                return _context.BillAccounts.ToList();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while retrieving all BillAccounts: " + ex.Message);
                return new List<BillAccount>();
            }
        }

        public void AddBillAccount(BillAccount billAccount)
        {
            try
            {
                _context.BillAccounts.Add(billAccount);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while adding BillAccount: " + ex.Message);
            }
        }

        public void UpdateBillAccount(BillAccount billAccount)
        {
            try
            {
                var existingBillAccount = _context.BillAccounts.Find(billAccount.BillAccountId);
                if (existingBillAccount != null)
                {
                    // Update properties
                    existingBillAccount.BillAccountNumber = billAccount.BillAccountNumber;
                    existingBillAccount.BillingType = billAccount.BillingType;
                    existingBillAccount.Status = billAccount.Status;
                    // Update other properties similarly

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while updating BillAccount: " + ex.Message);
            }
        }

        public void DeleteBillAccount(int id)
        {
            try
            {
                var billAccount = _context.BillAccounts.Find(id);
                if (billAccount != null)
                {
                    _context.BillAccounts.Remove(billAccount);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while deleting BillAccount: " + ex.Message);
            }
        }
    }

}
