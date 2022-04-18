using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class VerifyRecordModel
    {
        public long PoolID { get; set; }
        public long RecordID { get; set; }
        public int CustomerStage { get; set; }

    }

}
