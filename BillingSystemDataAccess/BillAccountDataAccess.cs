using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new Exception("An error occurred while retrieving BillAccount by Id.", ex);
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
                throw new Exception("An error occurred while retrieving BillAccount by Number.", ex);
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
                throw new Exception("An error occurred while retrieving all BillAccounts.", ex);
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
                throw new Exception("An error occurred while adding BillAccount.", ex);
            }
        }

        public void UpdateBillAccount(BillAccount updatedBillAccount)
        {
            try
            {
                var billAccountToUpdate = GetBillAccountByNumber(updatedBillAccount.BillAccountNumber);
                if (billAccountToUpdate != null)
                {
                    // Update properties
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
                throw new Exception("An error occurred while updating BillAccount.", ex);
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
                throw new Exception("An error occurred while deleting BillAccount.", ex);
            }
        }

        public void SuspendBillAccount(BillAccount billAccount)
        {
            try
            {
                BillAccount billAccountToSuspend = GetBillAccountById(billAccount.BillAccountId);
                billAccountToSuspend.Status = "Suspend";
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while suspending BillAccount.", ex);
            }
        }

        public void ReleaseBillAccount(BillAccount billAccount)
        {
            try
            {
                BillAccount billAccountToRelease = GetBillAccountById(billAccount.BillAccountId);
                billAccountToRelease.Status = "Active";
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while releasing BillAccount.", ex);
            }
        }
    }
}

