using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BillingSystemBusiness;
using BillingSystemDataModel;
using BillingSystemServices.Filters;
using log4net;

namespace BillingSystemServices.Controllers
{
    public class InstallmentController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InstallmentController));

        [RequestResponseLoggingFilter]
        [Route("api/CreateInstallmentSchedule")]
        [HttpPost]
        public IHttpActionResult CreateInstallmentSchedule(CreateInstallmentScheduleRequest request)
        {
            try
            {
                if (request == null || request.BillAccount == null || request.BillAccountPolicy == null || request.Premium == 0.0)
                {
                    return BadRequest("Invalid bill account data");
                }

                new InstallmentBusiness().CreateInstallmentSchedule(request.BillAccount, request.BillAccountPolicy, request.Premium);
                return Ok("Installments scheduled successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while creating installment schedule", ex);
                return InternalServerError(ex);
            }
        }

        public class CreateInstallmentScheduleRequest
        {
            public BillAccount BillAccount { get; set; }
            public BillAccountPolicy BillAccountPolicy { get; set; }
            public double Premium { get; set; }
        }
    }
}
