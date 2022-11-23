using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Members.Models
{
    public class memberDataList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StatusDescription { get; set; }
        public int? PolicyId { get; set; }
        public string PolicyType { get; set; }
        public decimal? PremiumAmout { get; set; }
        public string SubmittedDate { get; set; }
        public bool? IsPolicy { get; set; }







    }
}
