// <copyright file="BillAccountPolicyDataAccess.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BillingSystemDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BillingSystemDataModel;

    /// <summary>
    /// Provides data access methods for interacting with BillAccountPolicy entities.
    /// </summary>
    public class BillAccountPolicyDataAccess
    {
        private readonly BillingSystemEDMContainer context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BillAccountPolicyDataAccess"/> class.
        /// </summary>
        public BillAccountPolicyDataAccess()
        {
            this.context = new BillingSystemEDMContainer();
        }

        /// <summary>
        /// Retrieves a BillAccountPolicy entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BillAccountPolicy to retrieve.</param>
        /// <returns>The BillAccountPolicy entity corresponding to the provided ID, or null if not found.</returns>
        public BillAccountPolicy GetBillAccountPolicyById(int id)
        {
            try
            {
                return this.context.BillAccountPolicies.FirstOrDefault(b => b.BillAccountPolicyId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving BillAccountPolicy by Id.", ex);
            }
        }

        /// <summary>
        /// Retrieves all BillAccountPolicy entities.
        /// </summary>
        /// <returns>A list of all BillAccountPolicy entities.</returns>
        public List<BillAccountPolicy> GetAllBillAccountPolicies()
        {
            try
            {
                return this.context.BillAccountPolicies.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all BillAccountPolicies.", ex);
            }
        }

        /// <summary>
        /// Adds a new BillAccountPolicy entity.
        /// </summary>
        /// <param name="billAccountPolicy">The BillAccountPolicy entity to add.</param>
        public void AddBillAccountPolicy(BillAccountPolicy billAccountPolicy)
        {
            try
            {
                this.context.BillAccountPolicies.Add(billAccountPolicy);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding BillAccountPolicy.", ex);
            }
        }

        /// <summary>
        /// Deletes a BillAccountPolicy entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BillAccountPolicy to delete.</param>
        public void DeleteBillAccountPolicy(int id)
        {
            try
            {
                var billAccountPolicy = this.context.BillAccountPolicies.Find(id);
                if (billAccountPolicy != null)
                {
                    this.context.BillAccountPolicies.Remove(billAccountPolicy);
                    this.context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting BillAccountPolicy.", ex);
            }
        }
    }
}
