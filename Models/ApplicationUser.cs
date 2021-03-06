using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public int Role { get; set; }

    }
}
