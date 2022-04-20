
using Microsoft.AspNetCore.Identity;
using Stack.Entities.Models.Modules.Common;
using Stack.Entities.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.Entities.Models.Modules.Auth
{
    public class ApplicationUser : IdentityUser
    {

        [Column(TypeName = "varchar(70)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string LastName { get; set; }

        public string Status { get; set; }

        //public string ManagerID { get; set; } //Not migrated

        public virtual List<Contact> Contacts { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<Prospect> Prospects { get; set; }
        public virtual List<Lead> Leads { get; set; }
        public virtual List<Opportunity> Opportunities { get; set; }
        public virtual List<Pool_User> Pools { get; set; }
        public virtual List<PoolConnectionID> ConnectionIDs { get; set; }


    }

}
