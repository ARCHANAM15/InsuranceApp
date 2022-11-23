using Members.Models;
using Members.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Members.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        IMemberRegService memberRegService;

        public MemberController(IMemberRegService _memberRegService)
        {
            memberRegService = _memberRegService;
        }

        [HttpPost]
        [Route("add-member")]
        public IActionResult SaveMember([FromBody] MemberRegTbl member)
        {
            try
            {
                var data = memberRegService.SaveMember(member);
                return Ok(data);


            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }
        
        [HttpGet]
        [Route("GetAllPolicyStatus")]
        public IEnumerable<PolicyStatusTbl> GetAllPolicyStatus()
        {
            var policystatuslist = memberRegService.GetAllPolicyStatus();
            return policystatuslist;
        }
        [HttpPost]
        [Route("Search")]
        public IEnumerable<memberDataList> searchmember([FromBody] memberDataList memberDataList)
        {
            return memberRegService.Getsearchmember(memberDataList);
        }
        [HttpGet]
        [Route("getAllMember")]
        public IEnumerable<memberDataList> GetAllsearchmember()
        {
            return memberRegService.GetAllmember();
        }
    }
}
