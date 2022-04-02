using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LInterestInput : BaseEntity
    {
        public int Type { get; set; } //Enum
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public string Attachment { get; set; }

        public virtual List<LInterest_LInterestInput> LInterest_LInterestInputs { get; set; }

    }

}
