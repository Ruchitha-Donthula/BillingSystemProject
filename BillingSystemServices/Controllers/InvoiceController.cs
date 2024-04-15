using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BillingSystemBusiness;
using BillingSystemDataModel;
using BillingSystemServices.Filters;

namespace BillingSystemServices.Controllers
{
    public class InvoiceController : ApiController
    {
        [RequestResponseLoggingFilter]
        [Route("api/CreateInvoice")]
        [HttpPost]
        public IHttpActionResult CreateInvoice(BillAccount billAccount)
        {
            if (billAccount == null)
            {
                return BadRequest("Invalid bill account data");
            }

            new InvoiceBusiness().CreateInvoice(billAccount);
            return Ok("Invoice created successfully");
        }
    }
}