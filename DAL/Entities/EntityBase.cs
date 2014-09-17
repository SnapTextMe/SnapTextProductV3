using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnapTextWeb.DAL.Entities
{
    public class EntityBase
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedByUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedByUserId { get; set; }
    }
}