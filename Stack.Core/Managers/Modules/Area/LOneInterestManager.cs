using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LOneInterestManager : Repository<LOneInterest, ApplicationDbContext>
    {
        public LOneInterestManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
