using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class InterestAttributesManager : Repository<InterestAttribute, ApplicationDbContext>
    {
        public InterestAttributesManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
