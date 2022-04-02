using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models.Modules.AreaInterest
{
    public class LInterest_InterestAttributeModel
    {
        public long LInterestID { get; set; }
        public long InterestAttributeID { get; set; }

        public virtual LInterestModel LInterest { get; set; }

        public virtual InterestAttributeModel InterestAttribute { get; set; }

    }

}
