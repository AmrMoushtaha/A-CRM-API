using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class PoolAssignmentModel
    {
        public long PoolID { get; set; }
        public List<string> UserIDs { get; set; }


    }

}
