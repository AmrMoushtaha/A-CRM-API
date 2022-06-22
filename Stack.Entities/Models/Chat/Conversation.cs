using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Entities.Models.Modules.Chat
{
    public class Conversation :BaseEntity
    {

        public virtual List<Message> Messages { get; set; }
        public virtual List<UsersConversations> UsersConversations { get; set; }
    }

}
