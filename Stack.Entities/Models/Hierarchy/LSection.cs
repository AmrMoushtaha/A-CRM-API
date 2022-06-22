using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Hierarchy
{
    public class LSection : BaseEntity
    {
        public string LabelEN { get; set; }
        public string LabelAR { get; set; }

        public long LevelID { get; set; }
        [ForeignKey("LevelID")]
        public virtual Level Level { get; set; }

        public virtual List<Input> Inputs { get; set; }

    }

}
