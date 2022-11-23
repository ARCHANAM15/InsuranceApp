using Members.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Members.Services
{
    public class MemberServiceImp : IMemberRegService
    {
        HealthCareDBContext db;

        public MemberServiceImp(HealthCareDBContext _db)
        {
            db = _db;
        }

        public IEnumerable<PolicyStatusTbl> GetAllPolicyStatus()
        {
            var policystatuslist = db.PolicyStatusTbls.ToList();
            return policystatuslist;
        }

        public IEnumerable<memberDataList> GetAllmember()
        {
            var allmemberData = (from h in db.PolicyHistoryTbls
                                 join m in db.MemberRegTbls on h.MemberId equals m.Id
                                 join p in db.PolicyTbles on h.PolicyId equals p.MemberId 
                                 join ps in db.PolicyStatusTbls on h.PolicyStatusId equals ps.StatusId
                                 join l in db.LoginTbls on h.LoginId equals l.Id 
                                 where (h.IsPolicy==false
                                 )
                                 select new
                                 {
                                     memberId = m.Id,
                                     fisrtName = m.FirstName,
                                     lastName = m.LastName,
                                     policyId = h == null ? 0 : h.PrId,
                                     PolicyType = p == null ? "" : p.PolicyType,
                                     policyStatus = ps == null ? "" : ps.StatusDescription,
                                     PremiumAmount = p == null ? 0 : p.PremiumAmount,
                                     SubmittedDate = p == null ? "" : p.EffectiveDate.ToString(),
                                 }).ToList();
            memberDataList allmemberdataList;
            List<memberDataList> memberDataLists = new List<memberDataList>();
            foreach (var item in allmemberData)
            {
                allmemberdataList = new memberDataList();
                allmemberdataList.Id = item.memberId;
                allmemberdataList.FirstName = item.fisrtName;
                allmemberdataList.LastName = item.lastName;
                allmemberdataList.PolicyId = item.policyId;
                allmemberdataList.PolicyType = item.PolicyType;
                allmemberdataList.PremiumAmout = item.PremiumAmount;
                allmemberdataList.StatusDescription = item.policyStatus;
                memberDataLists.Add(allmemberdataList);

            }
            return memberDataLists;


        }

        public IEnumerable<memberDataList> Getsearchmember(memberDataList data)
        {
            var searchData = (from m in db.MemberRegTbls
                              join p in db.PolicyTbles on m.Id equals p.MemberId into xyz
                              from y in xyz.DefaultIfEmpty()
                              join ps in db.PolicyStatusTbls on y.StatusId equals ps.StatusId into stbl
                              from df in stbl.DefaultIfEmpty()
                              where ((m.Id == data.Id || m.FirstName == data.FirstName || m.LastName == data.LastName ||
                              y.PolicyId == data.PolicyId || df.StatusDescription == data.StatusDescription))
                              select new
                              {
                                  memberId = m.Id,
                                  fisrtName = m.FirstName,
                                  lastName = m.LastName,
                                  policyId = y == null ? 0 : y.PolicyId,
                                  PolicyType = y == null ? "" : y.PolicyType,
                                  policyStatus = df == null ? "" : df.StatusDescription,
                                  PremiumAmount = y == null ? 0 : y.PremiumAmount,
                                  SubmittedDate = y == null ? "" : y.EffectiveDate.ToString(),
                              }).ToList();
            memberDataList memberdataList;
            List<memberDataList> memberDataLists = new List<memberDataList>();
            foreach (var item in searchData)
            {
                memberdataList = new memberDataList();
                memberdataList.Id = item.memberId;
                memberdataList.FirstName = item.fisrtName;
                memberdataList.LastName = item.lastName;
                memberdataList.PolicyId = item.policyId;
                memberdataList.PolicyType = item.PolicyType;
                memberdataList.PremiumAmout = item.PremiumAmount;
                memberdataList.StatusDescription = item.policyStatus;
                memberDataLists.Add(memberdataList);

            }
            return memberDataLists;

        }


        public MemberRegTbl SaveMember(MemberRegTbl member)
        {
            try
            {
                member.CreateDate = DateTime.Now;
                member.ModifiedDate = DateTime.Now;
                db.MemberRegTbls.Add(member);
                db.SaveChanges();
                return member;
            }
            catch (Exception ex)
            {
                return member;
            }
        }
    }
}
