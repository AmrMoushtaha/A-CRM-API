using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class AttributeToAdd
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public bool IsPredefined { get; set; }
        public long ParentAttributeID { get; set; }


    }

}
