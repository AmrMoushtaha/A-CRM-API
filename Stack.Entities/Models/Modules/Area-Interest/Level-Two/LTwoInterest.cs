using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LTwoInterest
    {
        public long ID { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public virtual List<LTwoInterest_LTwoInterestInput> Inputs { get; set; }
        public virtual List<LTwoInterest_InterestAttributes> Attributes { get; set; }

    }

}
