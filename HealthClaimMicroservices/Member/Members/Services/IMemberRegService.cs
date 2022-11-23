using Members.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Members.Services
{
   public interface IMemberRegService
    {
        MemberRegTbl SaveMember(MemberRegTbl member);
        IEnumerable <memberDataList> Getsearchmember(memberDataList data);
        IEnumerable<memberDataList> GetAllmember();

        IEnumerable<PolicyStatusTbl> GetAllPolicyStatus();
        
    }
}
