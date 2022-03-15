using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LTwoInterest_InterestAttributeManager : Repository<LTwoInterest_InterestAttributes, ApplicationDbContext>
    {
        public LTwoInterest_InterestAttributeManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
