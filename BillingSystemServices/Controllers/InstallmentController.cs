using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BillingSystemBusiness;
using BillingSystemDataModel;

namespace BillingSystemServices.Controllers
{
    public class InstallmentController : ApiController
    {
        [Route("api/CreateInstallmentSchedule")]
        [HttpPost]
        public IHttpActionResult CreateInstallmentSchedule(CreateInstallmentScheduleRequest request)
        {
            if (request==null || request.billAccount == null || request.billAccountPolicy == null || request.premium == 0.0)
            {
                return BadRequest("Invalid bill account data");
            }

            new InstallmentBusiness().CreateInstallmentSchedule(request.billAccount, request.billAccountPolicy, request.premium);
            return Ok("Installments scheduled successfully");
        }
        public class CreateInstallmentScheduleRequest
        {
            public BillAccount billAccount { get; set; }
            public BillAccountPolicy billAccountPolicy { get; set; }
            public double premium { get; set; }
        }
    }
}