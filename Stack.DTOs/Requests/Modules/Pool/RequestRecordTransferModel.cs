using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class RequestTransferModel
    {
        public long CurrentPoolID { get; set; }
        public long? RequestedPoolID { get; set; }
        public string AssigneeID { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public long? RecordStatusID { get; set; }
        public int RecordType { get; set; }
    }

}
