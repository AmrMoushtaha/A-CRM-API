
using Stack.DTOs.Models.Modules.Pool;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models.Modules.Areas
{
    public class Location_PoolModel
    {
        public long LocationID { get; set; }
        public long PoolID { get; set; }
        public virtual LocationModel Location { get; set; }
        public virtual PoolViewModel Pool { get; set; }

    }

}
