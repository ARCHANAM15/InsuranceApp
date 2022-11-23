using Claim.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claim.Services
{
    public class PolicyAddServiceImp: IPolicyAddServices
    {
        HealthCareDBContext db;
        private IConfiguration _config;
        public PolicyAddServiceImp(IConfiguration config, HealthCareDBContext _db)
        {
            _config = config;
            db = _db;
        }

        public IEnumerable<PolicyStatusTbl> GetAllPolicyStatus()
        {
            var policystatuslist = db.PolicyStatusTbls.ToList();
            return policystatuslist;
        }

        public PolicyTble SubmitPolicy(PolicyTble PolicyTble)
        {
            try
            {
                PolicyTble.EffectiveDate = DateTime.Now;
                PolicyTble.CreatedDate = DateTime.Now;
                PolicyTble.ModifiedDate = DateTime.Now;
                Random generator = new Random();
                String r = generator.Next(0, 1000000).ToString("D6");
                PolicyTble.PolicyNumber = Convert.ToInt32(r);
                db.PolicyTbles.Add(PolicyTble);
                db.SaveChanges();
               
                if (PolicyTble != null)
                {
                    PolicyHistoryTbl historytbl = new PolicyHistoryTbl();
                    historytbl.LoginId = PolicyTble.LoginId;
                    historytbl.MemberId = PolicyTble.MemberId;
                    historytbl.PolicyStatusId = PolicyTble.StatusId;
                    historytbl.PolicyId = PolicyTble.PolicyId;
                    db.PolicyHistoryTbls.Add(historytbl);
                    db.SaveChanges();
                }
                return PolicyTble;
            }
            catch(Exception ex)
            {
                return PolicyTble;
            }
           


        }
    }
}
