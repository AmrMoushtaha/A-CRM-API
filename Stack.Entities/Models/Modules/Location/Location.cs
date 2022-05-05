using Stack.Entities.Models.Modules.Interest;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Areas
{
    public class Location : BaseEntity
    {
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public int LocationType { get; set; } //Area.. region.. LocationEnum
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public long? ParentLocationID { get; set; }
        [ForeignKey("ParentLocationID")]
        public virtual Location ParentLocation { get; set; }
       
        public List<Location_Pool> Pools { get; set; }
        public virtual List<LInterest> LInterests { get; set; }

    }

}
