using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models.Modules.AreaInterest
{
    public class InterestAttributeModel
    {
        public long ID { get; set; }
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }

        public virtual List<LInterest_InterestAttributeModel> LInterest_InterestAttributes { get; set; }


    }

}
