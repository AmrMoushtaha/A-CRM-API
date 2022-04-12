using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Lead
    {
        public long ID { get; set; }
        public string AssignedUserID { get; set; }
        public long DealID { get; set; }

        public int State { get; set; }
        public long? StatusID { get; set; }

        public bool IsFresh { get; set; }

        [ForeignKey("StatusID")]
        public virtual LeadStatus Status { get; set; }

        [ForeignKey("AssignedUserID")]
        public virtual ApplicationUser AssignedUser { get; set; }

        [ForeignKey("DealID")]
        public virtual Deal Deal { get; set; }
    }

}
