using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LTwoInterest_InterestAttributes
    {
        public long LTwoInterestID { get; set; }
        public long InterestAttributeID { get; set; }

        [ForeignKey("LTwoInterestID")]
        public virtual LTwoInterest LTwoInterest { get; set; }

        [ForeignKey("InterestAttributeID")]
        public virtual InterestAttribute InterestAttribute { get; set; }

    }

}
