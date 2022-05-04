using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class InputToAdd
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public int Type { get; set; } //Enum 
        public int? PredefinedInputType { get; set; } //Enum 

        public bool? IsRequired { get; set; }

        public long SectionID { get; set; }

        public long? AttributeID { get; set; }
        public List<AttributeToAdd> Attributes { get; set; }


    }

}
