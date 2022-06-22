
using Stack.DTOs.Models.Modules.Areas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models.Modules.AreaInterest
{
    public class LInterestModel
    {
        public long ID { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public bool IsSeparate { get; set; } // is resale
        public int Type { get; set; } //commercial ..... TypeEnum
        public int Level { get; set; } //Developer..project ... LevelEnum

        public long? OwnerID { get; set; } //if Separated
        
        public long LocationID { get; set; }
        public virtual LocationModel Location { get; set; }

        public long? ParentLInterestID { get; set; }
        public virtual LInterestModel ParentLInterest { get; set; }

        public virtual List<LInterest_LInterestInputModel> Inputs { get; set; }
        public virtual List<LInterest_InterestAttributeModel> Attributes { get; set; }

    }

}
