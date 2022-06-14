using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class LInterestToAdd
    {
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public bool IsSeparate { get; set; } // is resale

        public long LevelID { get; set; }
        public int? OwnerType { get; set; } //customer /contact enum 
        public long? OwnerID { get; set; } //if Separated
        public long? LocationID { get; set; }

        public long? ParentLInterestID { get; set; }


        public List<LInterestInputToAdd> LInterestInputs { get; set; }


    }

}
