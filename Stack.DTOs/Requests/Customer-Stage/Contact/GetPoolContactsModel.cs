using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.CustomerStage
{
    public class GetPoolContactsModel
    {
        public long PoolID { get; set; }

        public int PageNumber { get; set; }
    }

}
