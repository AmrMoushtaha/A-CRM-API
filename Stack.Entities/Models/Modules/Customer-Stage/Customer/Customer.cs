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
        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string FirstNameAR { get; set; }
        public string LastNameAR { get; set; }


        public virtual List<CustomerPhoneNumber> PhoneNumbers { get; set; }
        public virtual List<Contact> Contacts { get; set; }
        public virtual List<Deal> Deals { get; set; }
        public virtual List<ProcessFlow> ProcessFlows { get; set; }
        public virtual List<Customer_Tag> Tags { get; set; }


        [ForeignKey("AssignedUserID")]
        public virtual ApplicationUser AssignedUser { get; set; }

        public virtual List<LInterest> SeparatedLInterests { get; set; } 

    }

}
