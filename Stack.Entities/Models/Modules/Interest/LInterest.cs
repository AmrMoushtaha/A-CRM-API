using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Hierarchy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Interest
{
    public class LInterest : BaseEntity
    {
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public bool IsSeparate { get; set; } // is resale

        public long LevelID { get; set; } 
        [ForeignKey("LevelID")]
        public virtual Level Level { get; set; }

        public long? OwnerID { get; set; } //if Separated
        [ForeignKey("OwnerID")]
        public virtual Customer Owner { get; set; }
        
        public long? LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        public long? ParentLInterestID { get; set; }


        public virtual List<LInterestInput> LInterestInput { get; set; }

    }

}
