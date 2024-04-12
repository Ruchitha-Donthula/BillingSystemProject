using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingSystemDataModel;
using BillingSystemBusiness;
using System.Web.Http;

namespace BillingSystemServices.Controllers
{
    public class BillAccountController : ApiController
    {
        [HttpPost]
        [Route("api/CreateBillAccount")]
        public IHttpActionResult CreateBillAccount(BillAccount billAccount)
        {
            if (billAccount == null)
            {
                return BadRequest("Invalid bill account data");
            }

            new BillAccountBusiness().CreateBillAccount(billAccount);
            return Ok("BillAccount added successfully");
        }


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

        [Route("api/GetBillAccountById")]
        [HttpGet]
        public IHttpActionResult GetBillAccountById(int billAccountId)
        {
            var billAccount = new BillAccountBusiness().GetBillAccountById(billAccountId);
            if (billAccount == null)
            {
                return NotFound();
            }
            return Json(billAccount);
        }

        [Route("api/GetBillAccountByNumber")]
        [HttpGet]
        public IHttpActionResult GetBillAccountByNumber(string billAccountNumber)
        {
            var billAccount = new BillAccountBusiness().GetBillAccountByNumber(billAccountNumber);
            if (billAccount == null)
            {
                return NotFound();
            }
            return Json(billAccount);
        }

        [Route("api/UpdateBillAccount")]
        [HttpPost]
        public IHttpActionResult UpdateBillAccount(BillAccount billAccount)
        {
            if (billAccount == null)
            {
                return BadRequest("Invalid bill account data");
            }
            new BillAccountBusiness().UpdateBillAccount(billAccount);
            return Ok("Bill account updated successfully");
        }

        [Route("api/SuspendBillAccount")]
        [HttpPost]
        public IHttpActionResult SuspendBillAccount(BillAccount billAccount)
        {
            if (billAccount == null)
            {
                return BadRequest("Invalid bill account data");
            }
            new BillAccountBusiness().SuspendBillAccount(billAccount);
            return Ok("Bill account suspended successfully");
        }

        [Route("api/ReleaseBillAccount")]
        [HttpPost]
        public IHttpActionResult ReleaseBillAccount(BillAccount billAccount)
        {
            if (billAccount == null)
            {
                return BadRequest("Invalid bill account data");
            }
            new BillAccountBusiness().ReleaseBillAccount(billAccount);
            return Ok("Bill account released successfully");
        }
    }
}