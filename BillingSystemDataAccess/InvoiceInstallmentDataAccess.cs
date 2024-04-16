// <copyright file="InvoiceInstallmentDataAccess.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BillingSystemDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BillingSystemDataModel;

    /// <summary>
    /// Provides data access methods for interacting with InvoiceInstallment entities.
    /// </summary>
    public class InvoiceInstallmentDataAccess
    {
        private readonly BillingSystemEDMContainer context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceInstallmentDataAccess"/> class.
        /// </summary>
        public InvoiceInstallmentDataAccess()
        {
            this.context = new BillingSystemEDMContainer();
        }

        /// <summary>
        /// Retrieves an InvoiceInstallment entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the InvoiceInstallment to retrieve.</param>
        /// <returns>The InvoiceInstallment entity corresponding to the provided ID, or null if not found.</returns>
        public InvoiceInstallment GetInvoiceInstallmentById(int id)
        {
            try
            {
                return this.context.InvoiceInstallments.FirstOrDefault(i => i.InvoiceInstallmentId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving InvoiceInstallment by Id.", ex);
            }
        }

        /// <summary>
        /// Retrieves all InvoiceInstallment entities.
        /// </summary>
        /// <returns>A list of all InvoiceInstallment entities.</returns>
        public List<InvoiceInstallment> GetAllInvoiceInstallments()
        {
            try
            {
                return this.context.InvoiceInstallments.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all InvoiceInstallments.", ex);
            }
        }

        /// <summary>
        /// Adds a new InvoiceInstallment entity.
        /// </summary>
        /// <param name="invoiceInstallment">The InvoiceInstallment entity to add.</param>
        public void AddInvoiceInstallment(InvoiceInstallment invoiceInstallment)
        {
            try
            {
                this.context.InvoiceInstallments.Add(invoiceInstallment);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding InvoiceInstallment.", ex);
            }
        }

        /// <summary>
        /// Deletes an InvoiceInstallment entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the InvoiceInstallment to delete.</param>
        public void DeleteInvoiceInstallment(int id)
        {
            try
            {
                var invoiceInstallment = this.context.InvoiceInstallments.Find(id);
                if (invoiceInstallment != null)
                {
                    this.context.InvoiceInstallments.Remove(invoiceInstallment);
                    this.context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting InvoiceInstallment.", ex);
            }
        }
    }
}
