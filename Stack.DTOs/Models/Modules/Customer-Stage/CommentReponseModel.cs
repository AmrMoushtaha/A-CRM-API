using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class CommentReponseModel
    {
        public long ID { get; set; }

        public string Comment { get; set; }

        public long ContactID { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
