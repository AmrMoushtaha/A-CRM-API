using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class Area_LOneInterestManager : Repository<Area_LOneInterest, ApplicationDbContext>
    {
        public Area_LOneInterestManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
