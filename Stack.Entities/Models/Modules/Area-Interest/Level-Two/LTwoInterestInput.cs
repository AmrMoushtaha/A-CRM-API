using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LTwoInterestInput
    {
        public long ID { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }

        public virtual List<LTwoInterest_LTwoInterestInput> LTwoInterest_LTwoInterestInputs { get; set; }

    }

}
