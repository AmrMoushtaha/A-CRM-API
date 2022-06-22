using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Hierarchy
{
    public class Level : BaseEntity
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public int Type { get; set; }

        public virtual List<LSection> Sections { get; set; }

    }

}
