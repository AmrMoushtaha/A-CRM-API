using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Contact
    {
        public long ID { get; set; }


        public string FullNameEN { get; set; }
        public string FullNameAR { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string LeadSourceType { get; set; }
        public string LeadSourceName { get; set; }
        public string PrimaryPhoneNumber { get; set; }


        //Navitgation Properties . 
        public virtual ProcessFlow ProcessFlow { get; set; }
        public virtual List<ContactPhoneNumber> PhoneNumbers { get; set; }
        public virtual List<ContactComment> Comments { get; set; }
        public virtual List<Contact_Tag> Tags { get; set; }

        public long PoolID { get; set; }
        public long StatusID { get; set; }
        public string AssignedUserID { get; set; }


        [ForeignKey("StatusID")]
        public virtual ContactStatus Status { get; set; }

        [ForeignKey("AssignedUserID")]
        public virtual ApplicationUser AssignedUser { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("PoolID")]
        public virtual Pool Pool { get; set; }



    }

}
