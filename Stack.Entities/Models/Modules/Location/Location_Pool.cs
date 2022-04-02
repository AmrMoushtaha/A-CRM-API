using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Areas
{
    public class Location_Pool
    {
        public long LocationID { get; set; }
        public long PoolID { get; set; }


        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        [ForeignKey("PoolID")]
        public virtual Pool Pool { get; set; }

    }

}
