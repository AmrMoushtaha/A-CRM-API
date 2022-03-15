using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LThreeInterestManager : Repository<LThreeInterest, ApplicationDbContext>
    {
        public LThreeInterestManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
