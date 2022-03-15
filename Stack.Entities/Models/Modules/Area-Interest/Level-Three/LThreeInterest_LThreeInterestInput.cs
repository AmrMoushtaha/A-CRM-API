using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LThreeInterest_LThreeInterestInput
    {
        public long LThreeInterestID { get; set; }
        public long LThreeInterestInputID { get; set; }

        [ForeignKey("LThreeInterestID")]
        public virtual LThreeInterest LThreeInterest { get; set; }

        [ForeignKey("LThreeInterestInputID")]
        public virtual LThreeInterestInput LThreeInterestInput { get; set; }

    }

}
