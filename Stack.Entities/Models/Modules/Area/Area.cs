using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Areas
{
    public class Area
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public List<Area_Pool> Pools { get; set; }

        public virtual List<Area_LOneInterest> Interests { get; set; }

    }

}
