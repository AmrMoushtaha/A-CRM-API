using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LThreeInterest
    {
        public long ID { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public bool isStandalone { get; set; }


        public virtual List<LThreeInterest_LThreeInterestInput> Inputs { get; set; }
        public virtual List<LThreeInterest_InterestAttributes> Attributes { get; set; }
    }

}
