using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class AspUser : IdentityUser
    {
        //public int Id { get; set; }
        //public string UserName { get; set; }
        //public string NormalizedUserName { get; set; }
        //public string Email { get; set; }
        //public string NormalizedEmail { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string PasswordHash { get; set; }
        //public string SecurityStamp { get; set; }
        //public string ConcurrencyStamp { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public DateTime LockoutEnd { get; set; }
        //public bool LockoutEnabled { get; set; }
        //public int AccessFailedCount { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Status { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public virtual IList<CashFlow>? CashFlows { get; set; }
        public virtual IList<Blog>? Blogs { get; set; }
        public virtual IList<UserFunctionFarm>? UserFunctionFarms { get; set; }

    }
}
