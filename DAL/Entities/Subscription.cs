using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapTextWeb.DAL.Entities
{
    public class Subscription
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double monthlyAmount { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
