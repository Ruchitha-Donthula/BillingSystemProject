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
            _context = new BillingSystemEDMContainer();
        }

        public BillAccount GetBillAccountById(int id)
        {
            try
            {
                return _context.BillAccounts.FirstOrDefault(b => b.BillAccountId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while retrieving BillAccount by Id: " + ex.Message);
                return null;
            }
        }

        public BillAccount GetBillAccountByNumber(String billAccountNumber)
        {
            try
            {
                return _context.BillAccounts.FirstOrDefault(b => b.BillAccountNumber == billAccountNumber);
            }
            catch (Exception ex)
            {
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
                Console.WriteLine("Error occurred while adding BillAccount: " + ex.Message);
            }
        }

        public void UpdateBillAccount(BillAccount updatedBillAccount)
        {
            try
            {
                var billAccountToUpdate = GetBillAccountByNumber(updatedBillAccount.BillAccountNumber);
                if (billAccountToUpdate != null)
                {
                    billAccountToUpdate.BillingType = updatedBillAccount.BillingType;
                    billAccountToUpdate.Status = updatedBillAccount.Status;
                    billAccountToUpdate.PayorName = updatedBillAccount.PayorName;
                    billAccountToUpdate.PayorAddress = updatedBillAccount.PayorAddress;
                    billAccountToUpdate.PaymentMethod = updatedBillAccount.PaymentMethod;
                    billAccountToUpdate.DueDay = updatedBillAccount.DueDay;
                    billAccountToUpdate.AccountTotal = updatedBillAccount.AccountTotal;
                    billAccountToUpdate.AccountPaid = updatedBillAccount.AccountPaid;
                    billAccountToUpdate.AccountBalance = updatedBillAccount.AccountBalance;
                    billAccountToUpdate.LastPaymentDate = updatedBillAccount.LastPaymentDate;
                    billAccountToUpdate.LastPaymentAmount = updatedBillAccount.LastPaymentAmount;
                    billAccountToUpdate.PastDue = updatedBillAccount.PastDue;
                    billAccountToUpdate.FutureDue = updatedBillAccount.FutureDue;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
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
                Console.WriteLine("Error occurred while deleting BillAccount: " + ex.Message);
            }
        }

        public void SuspendBillAccount(BillAccount billAccount)
        {
            BillAccount billAccountToSuspend = GetBillAccountById(billAccount.BillAccountId);
            billAccountToSuspend.Status = "Suspend";
            _context.SaveChanges();
        }

        public void ReleaseBillAccount(BillAccount billAccount)
        {
            BillAccount billAccountToRelease = GetBillAccountById(billAccount.BillAccountId);
            billAccountToRelease.Status = "Active";
            _context.SaveChanges();
        }
    }
}
