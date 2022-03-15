using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Areas
{
    public class Area_Pool
    {
        public long AreaID { get; set; }
        public long PoolID { get; set; }


        [ForeignKey("AreaID")]
        public virtual Area Area { get; set; }

        [ForeignKey("PoolID")]
        public virtual Pool Pool { get; set; }

    }

}
