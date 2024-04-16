using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingSystemDataModel;
using BillingSystemBusiness;
using System.Web.Http;
using BillingSystemServices.Filters;
using log4net;

namespace BillingSystemServices.Controllers
{
    public class BillAccountController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BillAccountController));

        [RequestResponseLoggingFilter]
        [HttpPost]
        [Route("api/CreateBillAccount")]
        public IHttpActionResult CreateBillAccount(BillAccount billAccount)
        {
            try
            {
                if (billAccount == null)
                {
                    return BadRequest("Invalid bill account data");
                }

                new BillAccountBusiness().CreateBillAccount(billAccount);
                return Ok("BillAccount added successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                Log.Error("An error occurred while creating bill account", ex);
                // Return an Internal Server Error response
                return InternalServerError(ex);
            }
        }

        [RequestResponseLoggingFilter]
        [Route("api/AssociateBillAccountWithPolicy")]
        [HttpPost]
        public IHttpActionResult AssociateBillAccountWithPolicy(BillAccountAndPolicyRequest request)
        {
            if (request==null || request.BillAccount == null || request.PolicyNumbers == null || string.IsNullOrEmpty(request.Payplan))
            {
                return BadRequest("One or more parameters are null or empty.");
            }

            new BillAccountBusiness().AssociateBillAccountWithPolicy(request.BillAccount, request.PolicyNumbers, request.Payplan);
            return Ok("Bill account associated with policies successfully.");
        }

        public class BillAccountAndPolicyRequest
        {
            public BillAccount BillAccount { get; set; }
            public List<string> PolicyNumbers { get; set; }
            public string Payplan { get; set; }
        }

        [RequestResponseLoggingFilter]
        [Route("api/GetBillAccountById")]
        [HttpGet]
        public IHttpActionResult GetBillAccountById(int billAccountId)
        {
            try
            {
                var billAccount = new BillAccountBusiness().GetBillAccountById(billAccountId);
                if (billAccount == null)
                {
                    return NotFound();
                }
                return Json(billAccount);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while retrieving the bill account by ID", ex);
                return InternalServerError(ex);
            }
        }

        [RequestResponseLoggingFilter]
        [Route("api/GetBillAccountByNumber")]
        [HttpGet]
        public IHttpActionResult GetBillAccountByNumber(string billAccountNumber)
        {
            try
            {
                var billAccount = new BillAccountBusiness().GetBillAccountByNumber(billAccountNumber);
                if (billAccount == null)
                {
                    return NotFound();
                }
                return Json(billAccount);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while retrieving the bill account by number", ex);
                return InternalServerError(ex);
            }
        }

        [RequestResponseLoggingFilter]
        [Route("api/UpdateBillAccount")]
        [HttpPost]
        public IHttpActionResult UpdateBillAccount(BillAccount billAccount)
        {
            try
            {
                if (billAccount == null)
                {
                    return BadRequest("Invalid bill account data");
                }
                new BillAccountBusiness().UpdateBillAccount(billAccount);
                return Ok("Bill account updated successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while updating the bill account", ex);
                return InternalServerError(ex);
            }
        }

        [RequestResponseLoggingFilter]
        [Route("api/SuspendBillAccount")]
        [HttpPost]
        public IHttpActionResult SuspendBillAccount(BillAccount billAccount)
        {
            try
            {
                if (billAccount == null)
                {
                    return BadRequest("Invalid bill account data");
                }
                new BillAccountBusiness().SuspendBillAccount(billAccount);
                return Ok("Bill account suspended successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while suspending the bill account", ex);
                return InternalServerError(ex);
            }
        }

        [RequestResponseLoggingFilter]
        [Route("api/ReleaseBillAccount")]
        [HttpPost]
        public IHttpActionResult ReleaseBillAccount(BillAccount billAccount)
        {
            try
            {
                if (billAccount == null)
                {
                    return BadRequest("Invalid bill account data");
                }
                new BillAccountBusiness().ReleaseBillAccount(billAccount);
                return Ok("Bill account released successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while releasing the bill account", ex);
                return InternalServerError(ex);
            }
        }

    }
}