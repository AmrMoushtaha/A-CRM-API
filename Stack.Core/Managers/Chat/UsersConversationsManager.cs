using Stack.DAL;
using Stack.Entities.Models.Modules.Chat;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.chat
{
    public class UsersConversationsManager : Repository<UsersConversations, ApplicationDbContext>
    {

        public UsersConversationsManager(ApplicationDbContext _context) : base(_context)
        {
        }

    }

}
