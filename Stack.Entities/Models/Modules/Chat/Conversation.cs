using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Entities.Models.Modules.Chat
{
    public class Conversation
    {
        public long ID { get; set; }

        public virtual List<Message> Messages { get; set; }
        public virtual List<UsersConversations> UsersConversations { get; set; }
    }

}
