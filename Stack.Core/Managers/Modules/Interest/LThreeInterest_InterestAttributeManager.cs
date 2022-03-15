using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LThreeInterest_InterestAttributeManager : Repository<LThreeInterest_InterestAttributes, ApplicationDbContext>
    {
        public LThreeInterest_InterestAttributeManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
