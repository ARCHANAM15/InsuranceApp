using System;
using System.Collections.Generic;

#nullable disable

namespace Claim.Models
{
    public partial class PolicyTble
    {
        public int PolicyId { get; set; }
        public string PolicyType { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Remark { get; set; }
        public int? MemberId { get; set; }
        public int? StatusId { get; set; }
        public long? PolicyNumber { get; set; }
        public int? LoginId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

       
    }

    public partial class PolicyTbleDto
    {
        public int PolicyId { get; set; }
        public string PolicyType { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Remark { get; set; }
        public int? MemberId { get; set; }
        public int? StatusId { get; set; }
        public long? PolicyNumber { get; set; }
        public int? LoginId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string errorMsg { get; set; }
    }
}
