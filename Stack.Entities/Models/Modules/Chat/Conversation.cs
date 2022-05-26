using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Entities.Models.Modules.Chat
{
    public class Conversation
    {
        public long ID { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }

        public virtual List<Message> Messages { get; set; }
    }

}
