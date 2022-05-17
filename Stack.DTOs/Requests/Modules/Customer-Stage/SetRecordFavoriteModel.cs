using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class SetRecordFavoriteModel
    {

        public long RecordID { get; set; }

        public int CustomerStage { get; set; }

    }
}
