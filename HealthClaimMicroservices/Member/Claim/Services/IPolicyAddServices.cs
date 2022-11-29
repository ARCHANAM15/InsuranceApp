using Claim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claim.Services
{
   public interface IPolicyAddServices
    {
        PolicyTbleDto SubmitPolicy(PolicyTble PolicyTble);
        IEnumerable<PolicyStatusTbl> GetAllPolicyStatus();

        PolicyTble updatePolicy(PolicyTble PolicyTble);

        PolicyTble getMemberPolicyDtsById(int policyId);
    }
}
