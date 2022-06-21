using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.AreaInterest
{
    public class LocationToAdd
    {
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public int LocationType { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public long? ParentLocationID { get; set; }
    
    }

}
