using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class BulkContactCreationModel
    {
        public long PoolID { get; set; }

        public long? StatusID { get; set; }

        public List<string> AssignedUsers { get; set; }

        public List<ContactCreationModel> Contacts { get; set; }
    }

}
