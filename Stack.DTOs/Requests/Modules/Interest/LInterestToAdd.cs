using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class LInterestToAdd
    {
        public long ID { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public bool IsSeparate { get; set; }
        public int Type { get; set; }
        public int Level { get; set; }
        public long? OwnerID { get; set; }

        public long LocationID { get; set; }

        public long? ParentLInterestID { get; set; }

    }

}
