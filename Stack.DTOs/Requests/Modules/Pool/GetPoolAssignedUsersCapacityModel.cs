using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class GetPoolAssignedUsersCapacityModel
    {
        public long PoolID { get; set; }
        public List<string> AssignedUserIDs { get; set; }

    }

}
