using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.CustomerStage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class LInterest : BaseEntity
    {
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public bool IsSeparate { get; set; } // is resale
        public int Type { get; set; } //commercial ..... TypeEnum
        public int Level { get; set; } //Developer..project ... LevelEnum

        public long? OwnerID { get; set; } //if Separated
        [ForeignKey("OwnerID")]
        public virtual Customer Owner { get; set; }
        
        public long? LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        public long? ParentLInterestID { get; set; }
        [ForeignKey("ParentLInterestID")]
        public virtual LInterest ParentLInterest { get; set; }

        public virtual List<LInterest_LInterestInput> Inputs { get; set; }
        public virtual List<LInterest_InterestAttribute> Attributes { get; set; }

    }

}
