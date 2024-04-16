using System;
using System.Web.Http;
using BillingSystemBusiness;
using BillingSystemDataModel;
using BillingSystemServices.Filters;
using log4net;

namespace BillingSystemServices.Controllers
{
    public class PaymentController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PaymentController));

        [RequestResponseLoggingFilter]
        [Route("api/ApplyPayment")]
        [HttpPost]
        public IHttpActionResult ApplyPayment(Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return BadRequest("Invalid payment data");
                }

                new PaymentBusiness().ApplyPayment(payment);
                return Ok("Payment applied successfully");
            }
            catch (Exception ex)
            {
                // Log the exception using log4net
                Log.Error("An error occurred while applying payment", ex);

                // Return an Internal Server Error response
                return InternalServerError(ex);
            }
        }
    }
}
