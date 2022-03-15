using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LOneInterest_LOneInterestInput
    {
        public long LOneInterestID { get; set; }
        public long LOneInterestInputID { get; set; }

        [ForeignKey("LOneInterestID")]
        public virtual LOneInterest LOneInterest { get; set; }

        [ForeignKey("LOneInterestInputID")]
        public virtual LOneInterestInput LOneInterestInput { get; set; }

    }

}
