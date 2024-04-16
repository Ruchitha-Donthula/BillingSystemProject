using System;
using System.Linq;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class GetNextSequenceNumberFromDataBase
    {
        public int GetNextSequenceNumber()
        {
            try
            {
                var _dbContext = new BillingSystemEDMContainer();
                int nextSequenceNumber = 1; // Default if no records exist
                var latestBillAccount = _dbContext.BillAccounts.OrderByDescending(b => b.BillAccountId).FirstOrDefault();
                if (latestBillAccount != null)
                {
                    // Extract the numeric part and increment by 1
                    string numericPart = latestBillAccount.BillAccountNumber.Substring(2);
                    if (int.TryParse(numericPart, out int numericValue))
                    {
                        nextSequenceNumber = numericValue + 1;
                    }
                }
                return nextSequenceNumber;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the next sequence number from the database.", ex);
            }
        }
    }
}
