using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class GetFavoritesModel
    {
        public long? PoolID { get; set; }
        public int CustomerStage { get; set; }

    }
}
