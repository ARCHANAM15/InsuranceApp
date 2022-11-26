using Claim.Models;
using Claim.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class PolicySubmissionController : ControllerBase
    {
        IPolicyAddServices policyAddServices;
        public PolicySubmissionController(IPolicyAddServices _policyAddServices)
        {
            policyAddServices = _policyAddServices;
        }
        [HttpPost]
        [Route("submit-policy")]
      public IActionResult SubmitPolicy(PolicyTble PolicyTble)
        {
            try
            {
                var data = policyAddServices.SubmitPolicy(PolicyTble);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }
      [HttpPut]
      [Route("update-policy")]
        public IActionResult updatePolicy(PolicyTble policyTble)
        {
            try
            {
                var data = policyAddServices.updatePolicy(policyTble);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }




        [HttpGet]
        [Route("GetPolicyStatus")]
        public IEnumerable<PolicyStatusTbl> GetAllPolicyStatus()
        {
            var policystatuslist = policyAddServices.GetAllPolicyStatus();
            return policystatuslist;
        }

        [HttpGet]
        [Route("getMemberPolicyDtsById")]
        public PolicyTble getMemberPolicyDtsById(int policyId)
        {
            PolicyTble policyDts = policyAddServices.getMemberPolicyDtsById(policyId);
            return policyDts;
        }


    }
}
