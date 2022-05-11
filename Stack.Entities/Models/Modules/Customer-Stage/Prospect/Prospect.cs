using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Prospect
    {
        public long ID { get; set; }
        public string AssignedUserID { get; set; }
        public long DealID { get; set; }
        public int State { get; set; }
        public long? StatusID { get; set; }
        public long PoolID { get; set; }
        public bool IsFresh { get; set; }
        public bool IsLocked { get; set; }
        public string ForceUnlock_JobID { get; set; }

        [ForeignKey("StatusID")]
        public virtual ProspectStatus Status { get; set; }

        [ForeignKey("AssignedUserID")]
        public virtual ApplicationUser AssignedUser { get; set; }

        [ForeignKey("DealID")]
        public virtual Deal Deal { get; set; }
    }

}
