using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class UpatePoolUsersCapacityModel
    {
        public long PoolID { get; set; }
        public List<UpdatePoolUserCapacity> Users { get; set; }

    }

    public class UpdatePoolUserCapacity
    {
        public string UserID { get; set; }
        public int Capacity { get; set; }
    }


}
