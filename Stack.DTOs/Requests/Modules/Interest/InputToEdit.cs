using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class InputToEdit
    {
        public long ID { get; set; }
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public int Type { get; set; } 
        public bool IsRequired { get; set; }
        public bool IsLevelDependent { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public long SectionID { get; set; }

        public long? AttributeID { get; set; }


    }

}
