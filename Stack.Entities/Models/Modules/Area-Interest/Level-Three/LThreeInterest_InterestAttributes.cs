using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LThreeInterest_InterestAttributes
    {
        public long LThreeInterestID { get; set; }
        public long InterestAttributeID { get; set; }

        [ForeignKey("LThreeInterestID")]
        public virtual LThreeInterest LThreeInterest { get; set; }

        [ForeignKey("InterestAttributeID")]
        public virtual InterestAttribute InterestAttribute { get; set; }

    }

}
