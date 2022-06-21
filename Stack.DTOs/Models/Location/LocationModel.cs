
using Stack.DTOs.Models.Modules.AreaInterest;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Models.Modules.Areas
{
    public class LocationModel
    {
        public long ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public int LocationType { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public long? ParentLocationID { get; set; }
        public virtual LocationModel ParentLocation { get; set; }
       
        public List<Location_PoolModel> Pools { get; set; }
        public virtual List<LInterestModel> LInterests { get; set; }

    }

}
