using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class GetPoolRecordsModel
    {
        public long? PoolID { get; set; }
        public int RecordType { get; set; }
    }

}
