using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Hierarchy
{
    public class Input : BaseEntity
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public int Type { get; set; } //Enum 
        public int? PredefinedInputType { get; set; } //Enum 

        public bool IsRequired { get; set; } 
        //public bool IsLevelDependent { get; set; } 
        //public int MaxValue { get; set; } 
        //public int MinValue { get; set; } 

        public long SectionID { get; set; }
        [ForeignKey("SectionID")]
        public virtual LSection Section { get; set; }

        public long? AttributeID { get; set; }//predefined parent dropdown 
        [ForeignKey("AttributeID")]
        public virtual LAttribute Attribute { get; set; }


    }

}
