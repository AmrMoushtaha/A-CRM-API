using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LInterest_InterestAttribute
    {
        public long LInterestID { get; set; }
        public long InterestAttributeID { get; set; }

        [ForeignKey("LInterestID")]
        public virtual LInterest LInterest { get; set; }

        [ForeignKey("InterestAttributeID")]
        public virtual InterestAttribute InterestAttribute { get; set; }

    }

}
