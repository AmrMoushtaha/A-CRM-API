using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Chat
{
    public class Message : BaseEntity
    {

        public string SenderID { get; set; }

        public long ConversationID { get; set; }

        [ForeignKey("ConversationID")]
        public virtual Conversation Conversation { get; set; }

        public string Content { get; set; }
        public bool Seen { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
