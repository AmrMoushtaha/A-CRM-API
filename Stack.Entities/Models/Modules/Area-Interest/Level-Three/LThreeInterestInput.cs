using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LThreeInterestInput
    {
        public long ID { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }

        public virtual List<LThreeInterest_LThreeInterestInput> LThreeInterest_LThreeInterestInput { get; set; }

    }

}
