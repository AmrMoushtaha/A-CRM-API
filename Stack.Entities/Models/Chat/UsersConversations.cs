using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Chat
{
    public class UsersConversations :BaseEntity
    {

        public long ConversationID { get; set; }
        [ForeignKey("ConversationID")]
        public virtual Conversation Conversation { get; set; }

        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser User { get; set; }
    }

}
