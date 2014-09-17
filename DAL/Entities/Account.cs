using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SnapTextWeb.Models;

namespace SnapTextWeb.DAL.Entities
{
    public class Account
    {
        private SnapTextDataContext db = new SnapTextDataContext();

        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ContactName { get; set; }
        public string ContactEmailAddress { get; set; }
        public string ContactPhoneNumber { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }

        public Account Create(Account account)
        {
            account = db.Accounts.Add(account);
            db.SaveChanges();
            return account;
        }
    }
}
