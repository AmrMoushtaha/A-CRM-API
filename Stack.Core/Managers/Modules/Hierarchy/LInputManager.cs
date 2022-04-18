using Stack.DAL;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Hierarchy
{
    public class LInputManager : Repository<Input, ApplicationDbContext>
    {
        public LInputManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
