// <copyright file="InstallmentSummaryDataAccess.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BillingSystemDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BillingSystemDataModel;

    /// <summary>
    /// Provides data access methods for interacting with InstallmentSummary entities.
    /// </summary>
    public class InstallmentSummaryDataAccess
    {
        private readonly BillingSystemEDMContainer context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallmentSummaryDataAccess"/> class.
        /// </summary>
        public InstallmentSummaryDataAccess()
        {
            this.context = new BillingSystemEDMContainer();
        }

        /// <summary>
        /// Retrieves an InstallmentSummary entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the InstallmentSummary to retrieve.</param>
        /// <returns>The InstallmentSummary entity corresponding to the provided ID, or null if not found.</returns>
        public InstallmentSummary GetInstallmentSummaryById(int id)
        {
            try
            {
                return this.context.InstallmentSummaries.FirstOrDefault(i => i.InstallmentSummaryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving InstallmentSummary by Id.", ex);
            }
        }

        /// <summary>
        /// Retrieves all InstallmentSummary entities.
        /// </summary>
        /// <returns>A list of all InstallmentSummary entities.</returns>
        public List<InstallmentSummary> GetAllInstallmentSummaries()
        {
            try
            {
                return this.context.InstallmentSummaries.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all InstallmentSummaries.", ex);
            }
        }

        /// <summary>
        /// Adds a new InstallmentSummary entity.
        /// </summary>
        /// <param name="installmentSummary">The InstallmentSummary entity to add.</param>
        public void AddInstallmentSummary(InstallmentSummary installmentSummary)
        {
            try
            {
                this.context.InstallmentSummaries.Add(installmentSummary);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding InstallmentSummary.", ex);
            }
        }

        /// <summary>
        /// Updates an existing InstallmentSummary entity.
        /// </summary>
        /// <param name="installmentSummary">The updated InstallmentSummary entity.</param>
        public void UpdateInstallmentSummary(InstallmentSummary installmentSummary)
        {
            try
            {
                var existingInstallmentSummary = this.context.InstallmentSummaries.Find(installmentSummary.InstallmentSummaryId);
                if (existingInstallmentSummary != null)
                {
                    existingInstallmentSummary.PolicyNumber = installmentSummary.PolicyNumber;
                    existingInstallmentSummary.Status = installmentSummary.Status;
                    this.context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating InstallmentSummary.", ex);
            }
        }

        /// <summary>
        /// Deletes an InstallmentSummary entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the InstallmentSummary to delete.</param>
        public void DeleteInstallmentSummary(int id)
        {
            try
            {
                var installmentSummary = this.context.InstallmentSummaries.Find(id);
                if (installmentSummary != null)
                {
                    this.context.InstallmentSummaries.Remove(installmentSummary);
                    this.context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting InstallmentSummary.", ex);
            }
        }

        /// <summary>
        /// Retrieves a list of InstallmentSummary entities associated with a specific bill account ID.
        /// </summary>
        /// <param name="billAccountId">The ID of the bill account to retrieve installment summaries for.</param>
        /// <returns>A list of InstallmentSummary entities associated with the provided bill account ID.</returns>
        public List<InstallmentSummary> GetInstallmentSummariesByBillAccountId(int billAccountId)
        {
            try
            {
                // Filter InstallmentSummaries by BillAccountId
                var summaries = this.context.InstallmentSummaries.Where(summary => summary.BillAccountId == billAccountId).ToList();

                return summaries;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching installment summaries by BillAccountId.", ex);
            }
        }
    }
}
