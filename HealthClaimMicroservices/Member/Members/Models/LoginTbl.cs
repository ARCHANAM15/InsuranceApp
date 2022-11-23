using System;
using System.Collections.Generic;

#nullable disable

namespace Members.Models
{
    public partial class LoginTbl
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
