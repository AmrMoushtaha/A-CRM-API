using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LOneInterest
    {
        public long ID { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public virtual List<LOneInterest_LOneInterestInput> Inputs { get; set; }
        public virtual List<LTwoInterest> LTwoInterests { get; set; }
        public virtual List<Area_LOneInterest> Area_LOneInterests { get; set; }
        public virtual List<LOneInterest_InterestAttributes> Attributes { get; set; }

    }

}
