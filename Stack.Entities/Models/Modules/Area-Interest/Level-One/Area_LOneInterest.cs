using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class Area_LOneInterest
    {
        public long AreaID { get; set; }
        public long LOneInterestID { get; set; }


        [ForeignKey("AreaID")]
        public virtual Area Area { get; set; }

        [ForeignKey("LOneInterestID")]
        public virtual LOneInterest Interest { get; set; }

    }

}
