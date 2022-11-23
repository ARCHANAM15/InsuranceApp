using System;
using System.Collections.Generic;

#nullable disable

namespace Members.Models
{
    public partial class PolicyHistoryTbl
    {
        public int PrId { get; set; }
        public int? LoginId { get; set; }
        public int? PolicyId { get; set; }
        public int? PolicyStatusId { get; set; }
        public int? MemberId { get; set; }
        public bool? IsPolicy { get; set; }
        public long? PolicyNumber { get; set; }
    }
}
