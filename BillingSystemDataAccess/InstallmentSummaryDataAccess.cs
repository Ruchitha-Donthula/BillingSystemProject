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
                Console.WriteLine("Error occurred while retrieving InstallmentSummary by Id: " + ex.Message);
                return null;
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
                Console.WriteLine("Error occurred while retrieving all InstallmentSummaries: " + ex.Message);
                return new List<InstallmentSummary>();
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
                Console.WriteLine("Error occurred while adding InstallmentSummary: " + ex.Message);
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
                Console.WriteLine("Error occurred while updating InstallmentSummary: " + ex.Message);
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
                Console.WriteLine("Error occurred while deleting InstallmentSummary: " + ex.Message);
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
                Console.WriteLine("Error occurred while fetching installment summaries: " + ex.Message);
                return null;
            }
        }
    }
}
