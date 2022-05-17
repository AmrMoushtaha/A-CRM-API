
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Stack.DTOs.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Activities;
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

        public int Status { get; set; }

        public string SystemAuthorizations { get; set; }

        //public string ManagerID { get; set; } //Not migrated

        public virtual List<Contact> Contacts { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<Prospect> Prospects { get; set; }

        public virtual List<Activity> Activities { get; set; }
        public virtual List<Lead> Leads { get; set; }
        public virtual List<Opportunity> Opportunities { get; set; }
        public virtual List<Pool_User> Pools { get; set; }
        public virtual List<PoolConnectionID> ConnectionIDs { get; set; }
        public virtual List<Contact_Favorite> Contact_Favorites { get; set; }
        public virtual List<Prospect_Favorite> Prospect_Favorites { get; set; }
        public virtual List<Lead_Favorite> Lead_Favorites { get; set; }
        public virtual List<Opportunity_Favorite> Opportunity_Favorites { get; set; }
        public virtual List<DoneDeal_Favorite> DoneDeal_Favorites { get; set; }

        public AuthorizationsModel GetAuthModel()
        {

            if (SystemAuthorizations != null)
            {
                AuthorizationsModel AuthModel = JsonConvert.DeserializeObject<AuthorizationsModel>(SystemAuthorizations);

                return AuthModel;

            }
            else
            {
                return null;
            }

        }


    }

}
