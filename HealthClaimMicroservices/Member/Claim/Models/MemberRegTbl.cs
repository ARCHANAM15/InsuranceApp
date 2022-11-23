using System;
using System.Collections.Generic;

#nullable disable

namespace Claim.Models
{
    public partial class MemberRegTbl
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long MemberShipId { get; set; }
    }
}
