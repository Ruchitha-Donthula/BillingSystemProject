using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemBusiness
{
    public class BillAccountBusiness
    {
        public BillAccount CreateBillAccount(BillAccount billAccount)
        {
            string billAccountNumber = GenerateBillAccountNumber();
            billAccount.BillAccountNumber = billAccountNumber;
            new BillAccountDataAccess().AddBillAccount(billAccount);
            return billAccount;
        }

        private String GenerateBillAccountNumber()
        {
            int nextSequenceNumber = GetNextSequenceNumberFromDataBase();
            String billAccountNumber = $"BA{nextSequenceNumber:D6}";
            return billAccountNumber;
        }
        private int GetNextSequenceNumberFromDataBase()
        {
            int nextSequenceNumberFromDataBase= new GetNextSequenceNumberFromDataBase().GetNextSequenceNumber();
            return nextSequenceNumberFromDataBase;
        }

        public void AssociateBillAccountWithPolicy(BillAccount billAccount, List<string> policyNumbers, string payplan)
        {
            foreach (var policyNumber in policyNumbers)
            {
                BillAccountPolicy billAccountPolicy = new BillAccountPolicy
                {
                    BillAccountId = billAccount.BillAccountId,
                    PolicyNumber = policyNumber,
                    PayPlan=payplan
                };
                new BillAccountPolicyDataAccess().AddBillAccountPolicy(billAccountPolicy);
            }
        }

        public BillAccount GetBillAccountById(int billAccountId)
        {
           return  new BillAccountDataAccess().GetBillAccountById(billAccountId);

        }

        public BillAccount GetBillAccountByNumber(string billAccountNumber)
        {
            return new BillAccountDataAccess().GetBillAccountByNumber(billAccountNumber);

        }
        public  void UpdateBillAccount(BillAccount billAccount)
        {
            new BillAccountDataAccess().UpdateBillAccount(billAccount);
        }
        public void SuspendBillAccount(BillAccount billAccount)
        {
            new BillAccountDataAccess().SuspendBillAccount(billAccount);
        }
        public void ReleaseBillAccount(BillAccount billAccount)
        {
            new BillAccountDataAccess().ReleaseBillAccount(billAccount);
        }

    }
}
