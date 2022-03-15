using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LOneInterestInputManager : Repository<LOneInterestInput, ApplicationDbContext>
    {
        public LOneInterestInputManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
