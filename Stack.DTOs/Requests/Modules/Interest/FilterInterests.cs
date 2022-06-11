using Stack.DTOs.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class FilterInterests
    {
        public string SortingDirection { get; set; }
        public string SortingAttribute { get; set; }

        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public bool IsSeparate { get; set; } 

        public long LevelID { get; set; }

        public List<long> OwnerID { get; set; }

        public long LocationID { get; set; }

        public List<long> ParentLInterestID { get; set; }
        public List<long?> AttributeID { get; set; }



    }

}
