using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class NewDealCreationModel
    {
        public long? ContactID { get; set; }
        public long? CustomerID { get; set; }
        public long? StatusID { get; set; }
        public int RecordType { get; set; }

    }

}
