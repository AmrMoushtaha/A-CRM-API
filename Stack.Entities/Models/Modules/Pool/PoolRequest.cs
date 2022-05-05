using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class PoolRequest
    {
        public long ID { get; set; }
        public long PoolID { get; set; }
        public long? Requestee_PoolID { get; set; }
        public long? RecordID { get; set; }
        public int? RecordType { get; set; }
        public long? RecordStatusID { get; set; }
        public string RequesteeID { get; set; }
        public int RequestType { get; set; }
        public int Status { get; set; }

        public DateTime AppliedActionDate { get; set; }
        public DateTime RequestDate { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        [ForeignKey("PoolID")]
        public virtual Pool Pool { get; set; }

    }

}
