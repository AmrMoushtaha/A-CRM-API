using Stack.DAL;
using Stack.Entities.Models.Modules.Chat;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.chat
{
    public class MessageManager : Repository<Message, ApplicationDbContext>
    {

        public MessageManager(ApplicationDbContext _context) : base(_context)
        {
        }

    }

}
