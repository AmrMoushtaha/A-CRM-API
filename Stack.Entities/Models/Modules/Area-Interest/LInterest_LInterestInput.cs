using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LInterest_LInterestInput :BaseEntity
    {
        public long LInterestID { get; set; }
        public long LInterestInputID { get; set; }

        [ForeignKey("LInterestID")]
        public virtual LInterest LInterest { get; set; }

        [ForeignKey("LInterestInputID")]
        public virtual LInterestInput LInterestInput { get; set; }

    }

}
