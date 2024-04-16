using System;
using System.Collections.Generic;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemBusiness
{
    public class BillAccountBusiness
    {
        public BillAccount CreateBillAccount(BillAccount billAccount)
        {
            try
            {
                string billAccountNumber = GenerateBillAccountNumber();
                billAccount.BillAccountNumber = billAccountNumber;
                new BillAccountDataAccess().AddBillAccount(billAccount);
                return billAccount;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        private string GenerateBillAccountNumber()
        {
            try
            {
                int nextSequenceNumber = GetNextSequenceNumberFromDataBase();
                string billAccountNumber = $"BA{nextSequenceNumber:D6}";
                return billAccountNumber;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        private int GetNextSequenceNumberFromDataBase()
        {
            try
            {
                int nextSequenceNumberFromDataBase = new GetNextSequenceNumberFromDataBase().GetNextSequenceNumber();
                return nextSequenceNumberFromDataBase;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        public void AssociateBillAccountWithPolicy(BillAccount billAccount, List<string> policyNumbers, string payplan)
        {
            try
            {
                foreach (var policyNumber in policyNumbers)
                {
                    BillAccountPolicy billAccountPolicy = new BillAccountPolicy
                    {
                        BillAccountId = billAccount.BillAccountId,
                        PolicyNumber = policyNumber,
                        PayPlan = payplan
                    };
                    new BillAccountPolicyDataAccess().AddBillAccountPolicy(billAccountPolicy);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        public BillAccount GetBillAccountById(int billAccountId)
        {
            try
            {
                return new BillAccountDataAccess().GetBillAccountById(billAccountId);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        public BillAccount GetBillAccountByNumber(string billAccountNumber)
        {
            try
            {
                return new BillAccountDataAccess().GetBillAccountByNumber(billAccountNumber);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        public void UpdateBillAccount(BillAccount billAccount)
        {
            try
            {
                new BillAccountDataAccess().UpdateBillAccount(billAccount);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        public void SuspendBillAccount(BillAccount billAccount)
        {
            try
            {
                new BillAccountDataAccess().SuspendBillAccount(billAccount);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        public void ReleaseBillAccount(BillAccount billAccount)
        {
            try
            {
                new BillAccountDataAccess().ReleaseBillAccount(billAccount);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }
    }
}
