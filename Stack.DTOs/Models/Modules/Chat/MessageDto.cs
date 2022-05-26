using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.DTOs.Requests.Modules.Chat
{
    public class MessageDto
    {
        public string SenderID { get; set; }

        public long ConversationID { get; set; }

        public string Content { get; set; }


    }
}
