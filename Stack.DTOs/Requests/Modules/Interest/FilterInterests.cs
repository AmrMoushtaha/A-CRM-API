using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class FilterInterests
    {
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public bool IsSeparate { get; set; } 

        public long LevelID { get; set; }

        public long OwnerID { get; set; }

        public long LocationID { get; set; }

        public long ParentLInterestID { get; set; }



    }

}
