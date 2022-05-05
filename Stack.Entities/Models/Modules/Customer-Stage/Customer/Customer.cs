using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Interest;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Customer
    {
        public long ID { get; set; }
        public string AssignedUserID { get; set; }
        public long ContactID { get; set; }

        public string FullNameEN { get; set; }
        public string FullNameAR { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string LeadSourceType { get; set; }
        public string LeadSourceName { get; set; }
        public string PrimaryPhoneNumber { get; set; }


        public virtual List<CustomerPhoneNumber> PhoneNumbers { get; set; }
        public virtual List<Deal> Deals { get; set; }
        public virtual List<ProcessFlow> ProcessFlows { get; set; }
        public virtual List<Customer_Tag> Tags { get; set; }

        [ForeignKey("ContactID")]
        public virtual Contact Contact { get; set; }

        [ForeignKey("AssignedUserID")]
        public virtual ApplicationUser AssignedUser { get; set; }

        public virtual List<LInterest> SeparatedLInterests { get; set; }

    }

}
