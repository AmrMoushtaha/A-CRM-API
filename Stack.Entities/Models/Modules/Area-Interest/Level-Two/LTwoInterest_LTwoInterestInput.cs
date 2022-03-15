using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LTwoInterest_LTwoInterestInput
    {
        public long LTwoInterestID { get; set; }
        public long LTwoInterestInputID { get; set; }

        [ForeignKey("LTwoInterestID")]
        public virtual LTwoInterest LTwoInterest { get; set; }

        [ForeignKey("LTwoInterestInputID")]
        public virtual LTwoInterestInput LTwoInterestInput { get; set; }

    }

}
