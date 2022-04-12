using Stack.DTOs.Models.Modules.Pool;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.General
{

    public class StageChangeModel
    {
        public List<StageModel> Stages { get; set; }

    }

    public class StageModel
    {
        public string StageNameEN { get; set; }
        public string StageNameAR { get; set; }
     
        public List<StatusModel> Statuses { get; set; }

    }


    public class StatusModel
    {
        public long ID { get; set; }
        public string EN { get; set; }
        public string AR { get; set; }

    }



}
