using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class AttributeToEdit
    {
        public long ID { get; set; }
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public long? ParentAttributeID { get; set; }

    }

}
