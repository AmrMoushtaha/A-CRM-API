using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class PoolRequestModel
    {
        public long ID { get; set; }
        public long PoolID { get; set; }
        public long? RecordID { get; set; }
        public string RequesteeID { get; set; }
        public int RequestType { get; set; }
        public int Status { get; set; }

        public DateTime RequestDate { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public string PoolNameEN { get; set; }
        public string PoolNameAR { get; set; }

    }


}
