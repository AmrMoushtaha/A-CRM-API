using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models.Modules.AreaInterest
{
    public class LInterest_LInterestInputModel
    {
        public long LInterestID { get; set; }
        public long LInterestInputID { get; set; }
        public virtual LInterestModel LInterest { get; set; }
        public virtual LInterestInputModel LInterestInput { get; set; }

    }

}
