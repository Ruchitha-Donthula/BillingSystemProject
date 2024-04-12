using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BillingSystemBusiness;
using BillingSystemDataModel;

namespace BillingSystemServices.Controllers
{
    public class PaymentController : ApiController
    {
        [Route("api/ApplyPayment")]
        [HttpPost]
        public IHttpActionResult ApplyPayment(Payment payment)
        {
            if (payment == null)
            {
                return BadRequest("Invalid bill account data");
            }

            new PaymentBusiness().ApplyPayment(payment);
            return Ok("Payment applied successfully");
        }
    }
}