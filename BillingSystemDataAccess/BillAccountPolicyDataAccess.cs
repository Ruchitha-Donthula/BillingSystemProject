using System;
using System.Collections.Generic;
using System.Linq;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class BillAccountPolicyDataAccess
    {
        private readonly BillingSystemEDMContainer _context;

        public BillAccountPolicyDataAccess()
        {
            _context = new BillingSystemEDMContainer();
        }

        public BillAccountPolicy GetBillAccountPolicyById(int id)
        {
            try
            {
                return _context.BillAccountPolicies.FirstOrDefault(b => b.BillAccountPolicyId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving BillAccountPolicy by Id.", ex);
            }
        }

        public List<BillAccountPolicy> GetAllBillAccountPolicies()
        {
            try
            {
                return _context.BillAccountPolicies.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all BillAccountPolicies.", ex);
            }
        }

        public void AddBillAccountPolicy(BillAccountPolicy billAccountPolicy)
        {
            try
            {
                _context.BillAccountPolicies.Add(billAccountPolicy);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding BillAccountPolicy.", ex);
            }
        }

        public void DeleteBillAccountPolicy(int id)
        {
            try
            {
                var billAccountPolicy = _context.BillAccountPolicies.Find(id);
                if (billAccountPolicy != null)
                {
                    _context.BillAccountPolicies.Remove(billAccountPolicy);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting BillAccountPolicy.", ex);
            }
        }
    }
}
