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

        public PolicyTble getMemberPolicyDtsById(int policyId)
        {
            var policyDts = db.PolicyTbles.Where(x => x.PolicyId == Convert.ToInt32(policyId)).FirstOrDefault();
            return policyDts;
        }

        public PolicyTbleDto SubmitPolicy(PolicyTble PolicyTble)
        {
            PolicyTbleDto dto = new PolicyTbleDto();
            
            try
            {
               
                var Insurance = db.PolicyTbles.Where(x => x.MemberId == PolicyTble.MemberId && x.PolicyType == PolicyTble.PolicyType).FirstOrDefault();
                if (Insurance != null)
                {
                    dto.errorMsg = "You already have this insurance in your account.";
                    return dto;
                }
                
                PolicyTble.EffectiveDate = DateTime.Now;
                PolicyTble.CreatedDate = DateTime.Now;
                PolicyTble.ModifiedDate = DateTime.Now;
                Random generator = new Random();
                String r = generator.Next(0, 1000000).ToString("D6");
                PolicyTble.PolicyNumber = Convert.ToInt32(r);
                dto.PolicyNumber = PolicyTble.PolicyNumber;
               
                db.PolicyTbles.Add(PolicyTble);
                db.SaveChanges();
                
                if (PolicyTble != null)
                {
                    PolicyHistoryTbl historytbl = new PolicyHistoryTbl();
                    historytbl.IsPolicy = false;
                    historytbl.LoginId = PolicyTble.LoginId;
                    historytbl.MemberId = PolicyTble.MemberId;
                    historytbl.PolicyStatusId = PolicyTble.StatusId;
                    historytbl.PolicyId = PolicyTble.PolicyId;
                    db.PolicyHistoryTbls.Add(historytbl);
                    db.SaveChanges();
                }
                return dto;
            }
            catch(Exception ex)
            {
                return dto;
            }
           


        }

        public PolicyTble updatePolicy(PolicyTble PolicyTble)
        {
            try
            {
                
                var policyupdate = db.PolicyTbles.Where(x => x.PolicyId == PolicyTble.PolicyId).FirstOrDefault();
                policyupdate.PremiumAmount = PolicyTble.PremiumAmount;
                policyupdate.Remark = PolicyTble.Remark;
                policyupdate.ModifiedDate = DateTime.Now;
                policyupdate.EffectiveDate = DateTime.Now;
                policyupdate.StatusId = PolicyTble.StatusId;
                db.PolicyTbles.Update(policyupdate);
                db.SaveChanges();
                if (policyupdate != null)
                {
                    var historyupdate = db.PolicyHistoryTbls.Where(x => x.PolicyId == policyupdate.PolicyId).FirstOrDefault();
                    PolicyHistoryTbl historytbl = new PolicyHistoryTbl();

                    historyupdate.PolicyStatusId = policyupdate.StatusId;
                    
                    db.PolicyHistoryTbls.Update(historytbl);
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
