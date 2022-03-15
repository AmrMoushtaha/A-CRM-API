using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LThreeInterestInputManager : Repository<LThreeInterestInput, ApplicationDbContext>
    {
        public LThreeInterestInputManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
