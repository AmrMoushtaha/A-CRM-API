using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Contact
    {
        public long ID { get; set; }
        public long PoolID { get; set; }
        public string AssignedUserID { get; set; }

        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string FirstNameAR { get; set; }
        public string LastNameAR { get; set; }

        public string Status { get; set; }

        public virtual List<ContactPhoneNumber> PhoneNumbers { get; set; }

        [ForeignKey("AssignedUserID")]
        public virtual ApplicationUser AssignedUser { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("PoolID")]
        public virtual Pool Pool { get; set; }
    }

}
