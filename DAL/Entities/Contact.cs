using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapTextWeb.DAL.Entities
{
    public class Contact
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? OptedInDate { get; set; }

        public virtual Account Account { get; set; }
    }
}
