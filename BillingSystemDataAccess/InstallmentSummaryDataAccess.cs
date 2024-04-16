using System;
using System.Collections.Generic;
using System.Linq;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class InstallmentSummaryDataAccess
    {
        private readonly BillingSystemEDMContainer _context;

        public InstallmentSummaryDataAccess()
        {
            _context = new BillingSystemEDMContainer();
        }

        public InstallmentSummary GetInstallmentSummaryById(int id)
        {
            try
            {
                return _context.InstallmentSummaries.FirstOrDefault(i => i.InstallmentSummaryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving InstallmentSummary by Id.", ex);
            }
        }

        public List<InstallmentSummary> GetAllInstallmentSummaries()
        {
            try
            {
                return _context.InstallmentSummaries.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all InstallmentSummaries.", ex);
            }
        }

        public void AddInstallmentSummary(InstallmentSummary installmentSummary)
        {
            try
            {
                _context.InstallmentSummaries.Add(installmentSummary);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding InstallmentSummary.", ex);
            }
        }

        public void UpdateInstallmentSummary(InstallmentSummary installmentSummary)
        {
            try
            {
                var existingInstallmentSummary = _context.InstallmentSummaries.Find(installmentSummary.InstallmentSummaryId);
                if (existingInstallmentSummary != null)
                {
                    existingInstallmentSummary.PolicyNumber = installmentSummary.PolicyNumber;
                    existingInstallmentSummary.Status = installmentSummary.Status;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating InstallmentSummary.", ex);
            }
        }

        public void DeleteInstallmentSummary(int id)
        {
            try
            {
                var installmentSummary = _context.InstallmentSummaries.Find(id);
                if (installmentSummary != null)
                {
                    _context.InstallmentSummaries.Remove(installmentSummary);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting InstallmentSummary.", ex);
            }
        }

        public List<InstallmentSummary> GetInstallmentSummariesByBillAccountId(int billAccountId)
        {
            try
            {
                // Filter InstallmentSummaries by BillAccountId
                var summaries = _context.InstallmentSummaries.Where(summary => summary.BillAccountId == billAccountId).ToList();

                return summaries;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching installment summaries by BillAccountId.", ex);
            }
        }
    }
}
