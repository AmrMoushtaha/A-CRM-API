using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models.Modules.AreaInterest
{
    public class LInterestInputModel
    {
        public long ID { get; set; }
        public int Type { get; set; } //Enum
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public string Attachment { get; set; }

        public virtual List<LInterest_LInterestInputModel> LInterest_LInterestInputs { get; set; }

    }

}
