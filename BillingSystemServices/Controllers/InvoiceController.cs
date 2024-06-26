﻿// <copyright file="InvoiceController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BillingSystemServices.Controllers
{
    using System;
    using System.Web.Http;
    using BillingSystemBusiness;
    using BillingSystemDataModel;
    using BillingSystemServices.Filters;
    using log4net;

    /// <summary>
    /// Controller for managing invoices.
    /// </summary>
    public class InvoiceController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InvoiceController));

        /// <summary>
        /// Creates an invoice for the specified bill account.
        /// </summary>
        /// <param name="billAccount">The bill account for which the invoice is created.</param>
        /// <returns>IHttpActionResult.</returns>
        [RequestResponseLoggingFilter]
        [Route("api/CreateInvoice")]
        [HttpPost]
        public IHttpActionResult CreateInvoice(BillAccount billAccount)
        {
            try
            {
                if (billAccount == null)
                {
                    return this.BadRequest("Invalid bill account data");
                }

                new InvoiceBusiness().CreateInvoice(billAccount);
                return this.Ok("Invoice created successfully");
            }
            catch (Exception ex)
            {
                // Log the exception using log4net
                Log.Error("An error occurred while creating invoice", ex);

                // Return an Internal Server Error response
                return this.InternalServerError(ex);
            }
        }
    }
}
