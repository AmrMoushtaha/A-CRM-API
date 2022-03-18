using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class AddCommentModel
    {
        public long ReferenceID { get; set; }
        public string Comment { get; set; }

    }

}
