using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class TagsDTO
    {
        public long ID { get; set; }
        public string Title { get; set; }

    }

    public class TagAppendanceModel
    {
        public long ReferenceID { get; set; }
        public long TagID { get; set; }
    }

}
