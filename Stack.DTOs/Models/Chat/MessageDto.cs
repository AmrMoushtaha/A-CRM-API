using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.DTOs.Requests.Modules.Chat
{
    public class MessageDto
    {
        public long ID { get; set; }
        public string SenderID { get; set; }

        public long ConversationID { get; set; }

        public virtual ConversationDto Conversation { get; set; }

        public string Content { get; set; }
        public bool Seen { get; set; }
        public DateTime Timestamp { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
