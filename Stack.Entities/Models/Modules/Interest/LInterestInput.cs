using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Hierarchy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Interest
{
    public class LInterestInput : BaseEntity
    {
        public string Attachment { get; set; }
        public long? SelectedAttributeID { get; set; }

        public long InputID { get; set; }
      
        public long LInterestID { get; set; }
        [ForeignKey("LInterestID")]
        public virtual LInterest LInterest { get; set; }

        // input properties
        //public string LabelAR { get; set; }
        //public string LabelEN { get; set; }
        //public int Type { get; set; } 
        //public int? PredefinedInputType { get; set; }  
        //public bool IsRequired { get; set; }
        //public long SectionID { get; set; }
        //public long? AttributeID { get; set; }
        // end 
    }

}
