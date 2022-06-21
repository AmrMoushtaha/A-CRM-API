using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.DTOs.Requests.Modules.Chat
{
    public class ConversationDto
    {
        public long ID { get; set; }

        public virtual List<MessageDto> Messages { get; set; }
        public virtual List<UsersConversationsDto> UsersConversations { get; set; }

    }
}
