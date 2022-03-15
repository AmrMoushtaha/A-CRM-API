using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Region
{
    public class Region
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public virtual List<Area> Areas { get; set; }

    }

}
