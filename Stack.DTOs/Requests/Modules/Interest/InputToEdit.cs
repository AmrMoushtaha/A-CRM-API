using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.AreaInterest
{
    public class InputToEdit
    {
        public long ID { get; set; } 
        public int Type { get; set; } 
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public string Attachment { get; set; }


    }

}
