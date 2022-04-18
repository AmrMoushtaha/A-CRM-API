using Stack.DAL;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Interest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Interest
{
    public class LInterestInputManager : Repository<LInterestInput, ApplicationDbContext>
    {
        public LInterestInputManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
