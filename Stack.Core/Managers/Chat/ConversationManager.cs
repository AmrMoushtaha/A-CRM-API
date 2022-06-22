using Stack.DAL;
using Stack.Entities.Models.Modules.Chat;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.chat
{
    public class ConversationManager : Repository<Conversation, ApplicationDbContext>
    {

        public ConversationManager(ApplicationDbContext _context) : base(_context)
        {
        }

    }

}
