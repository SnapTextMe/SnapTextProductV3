using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

namespace SnapTextWeb.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual User User { get; set; }
    }

    public class User
    {
        public long Id { get; set; }
        //public virtual SnapTextWeb.Models.ApplicationUser AspNetUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Account Account { get; set; }
    }
}
