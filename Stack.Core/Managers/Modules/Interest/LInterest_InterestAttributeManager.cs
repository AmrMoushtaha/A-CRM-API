using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Interest
{
    public class LInterest_InterestAttributeManager : Repository<LInterest_InterestAttribute, ApplicationDbContext>
    {
        public LInterest_InterestAttributeManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
