using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LThreeInterest_LThreeInterestInputManager : Repository<LThreeInterest_LThreeInterestInput, ApplicationDbContext>
    {
        public LThreeInterest_LThreeInterestInputManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
