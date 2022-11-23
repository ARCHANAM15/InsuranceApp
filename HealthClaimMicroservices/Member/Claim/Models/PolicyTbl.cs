using System;
using System.Collections.Generic;

#nullable disable

namespace Claim.Models
{
    public partial class PolicyTbl
    {
        public int PolicyId { get; set; }
        public string PolicyType { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Remark { get; set; }
        public int? MemberId { get; set; }
        public int? StatusId { get; set; }
        public bool? IsPolicyStatus { get; set; }
    }
}
