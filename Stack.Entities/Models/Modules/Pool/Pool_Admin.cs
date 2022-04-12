using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Pool_Admin
    {
        public string UserID { get; set; }
        public long PoolID { get; set; }
        public int? Capacity { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("PoolID")]
        public virtual Pool Pool { get; set; }

    }

}
