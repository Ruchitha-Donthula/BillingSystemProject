using System;
using System.Web.Http;
using BillingSystemBusiness;
using BillingSystemDataModel;
using BillingSystemServices.Filters;
using log4net;

namespace BillingSystemServices.Controllers
{
    public class InvoiceController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InvoiceController));

        [RequestResponseLoggingFilter]
        [Route("api/CreateInvoice")]
        [HttpPost]
        public IHttpActionResult CreateInvoice(BillAccount billAccount)
        {
            try
            {
                if (billAccount == null)
                {
                    return BadRequest("Invalid bill account data");
                }

                new InvoiceBusiness().CreateInvoice(billAccount);
                return Ok("Invoice created successfully");
            }
            catch (Exception ex)
            {
                // Log the exception using log4net
                Log.Error("An error occurred while creating invoice", ex);

                // Return an Internal Server Error response
                return InternalServerError(ex);
            }
        }
    }
}
