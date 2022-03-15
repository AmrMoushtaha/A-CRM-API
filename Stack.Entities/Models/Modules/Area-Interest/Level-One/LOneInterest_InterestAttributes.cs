using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LOneInterest_InterestAttributes
    {
        public long LOneInterestID { get; set; }
        public long InterestAttributeID { get; set; }

        [ForeignKey("LOneInterestID")]
        public virtual LOneInterest LOneInterest { get; set; }

        [ForeignKey("InterestAttributeID")]
        public virtual InterestAttribute InterestAttribute { get; set; }

    }

}
