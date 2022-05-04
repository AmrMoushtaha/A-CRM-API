using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Hierarchy
{
    public class LAttribute : BaseEntity
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public bool IsPredefined { get; set; }

        public long? ParentAttributeID { get; set; }
        [ForeignKey("ParentAttributeID")]
        public virtual LAttribute ParentAttribute { get; set; }

        public long? ParentInputID { get; set; } //dropdown etc..

        public virtual List<Input> Inputs { get; set; }


    }

}
