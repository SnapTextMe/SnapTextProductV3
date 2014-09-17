using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SnapTextWeb.DAL.Entities;

namespace SnapTextWeb.DAL
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SnapTextDataContext>
    {
        protected override void Seed(SnapTextDataContext context)
        {
            // Subscriptions.
            var subscriptions = new List<Subscription>{
                new Subscription{Name="Default", monthlyAmount=150.00}
            };

            subscriptions.ForEach(s => context.Subscriptions.Add(s));
            context.SaveChanges();

            // Accounts.
            var accounts = new List<Account>{
                new Account{Name="Country Inn & Suites by Carlson at ASU/Phoenix Airport", DisplayName="Country Inn & Suites by Carlson at ASU/Phoenix Airport", ContactName="Shelly Leydecker", ContactEmailAddress="jim@snaptext.me", ContactPhoneNumber="6023732559"}
            };

            // Account subscriptions.
            //accounts.ForEach(a => a.Subscriptions.Add(subscriptions[0]));
            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();

            //// Account contacts.
            //var contacts = new List<Contact>{
            //    new Contact{FirstName="Fred", LastName="Flinstone", OptedInDate=DateTime.UtcNow},
            //    new Contact{FirstName="Wilma", LastName="Flinstone", OptedInDate=DateTime.UtcNow},
            //    new Contact{FirstName="Barney", LastName="Rubble", OptedInDate=DateTime.UtcNow},
            //    new Contact{FirstName="Betty", LastName="Flinstone", OptedInDate=DateTime.UtcNow}
            //};

            //accounts.ForEach(a => a.Contacts.Add(contacts[0]));
            //contacts.ForEach(c => context.Contacts.Add(c));
            //context.SaveChanges();
        }
    }
}
