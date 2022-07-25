using Stack.DTOs.Models.Modules.Pool;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.General
{
    public class GetTeamRecords
    {
        public long? PoolID { get; set; }
        public int CustomerStage { get; set; }
        public int State { get; set; }
    }
}
