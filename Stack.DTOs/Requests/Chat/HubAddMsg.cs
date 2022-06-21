using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Shared
{
    public class HubAddMsg
    {
        public string ReceiverID { get; set; }

        public long ConversationID { get; set; }

        public string Content { get; set; }

    }

}
