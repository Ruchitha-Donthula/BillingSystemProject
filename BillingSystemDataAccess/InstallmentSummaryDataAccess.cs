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
            _context = new BillingSystemEDMContainer(); // Assuming you have a DbContext named BillingSystemContext
        }

        public InstallmentSummary GetInstallmentSummaryById(int id)
        {
            try
            {
                return _context.InstallmentSummaries.FirstOrDefault(i => i.InstallmentSummaryId == id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
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
                // Handle or log the exception
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
                // Handle or log the exception
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
                    // Update properties
                    existingInstallmentSummary.PolicyNumber = installmentSummary.PolicyNumber;
                    existingInstallmentSummary.Status = installmentSummary.Status;
                    // Update other properties similarly

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
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
                // Handle or log the exception
                Console.WriteLine("Error occurred while deleting InstallmentSummary: " + ex.Message);
            }
        }

        /*
        public void PrintInstallmentsInSummary(int summaryId)
        {
            try
            {
                var summary = _context.InstallmentSummaries.Find(summaryId); // Find the summary by its ID
                if (summary != null)
                {
                    Console.WriteLine($"Installments in Summary with ID {summaryId}:");
                    foreach (var installment in summary.Installments)
                    {
                        Console.WriteLine($"Installment ID: {installment.InstallmentId}, Sequence Number: {installment.InstallmentSequenceNumber}, Due Amount: {installment.DueAmount}");
                        // Print other installment properties as needed
                    }
                }
                else
                {
                    Console.WriteLine($"Installment Summary with ID {summaryId} not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while printing installments: " + ex.Message);
            }
        }
        */
    }
}
