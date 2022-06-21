using Stack.DTOs.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.DTOs.Requests.Modules.Chat
{
    public class UsersConversationsDto 
    {
        public long ID { get; set; }

        public long ConversationID { get; set; }

        public virtual ConversationDto Conversation { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUserDTO User { get; set; }

    }
}
