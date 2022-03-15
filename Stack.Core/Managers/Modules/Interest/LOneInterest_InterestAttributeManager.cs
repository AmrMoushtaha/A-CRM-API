using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LOneInterest_InterestAttributeManager : Repository<LOneInterest_InterestAttributes, ApplicationDbContext>
    {
        public LOneInterest_InterestAttributeManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
