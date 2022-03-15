using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LTwoInterest_LTwoInterestInputManager : Repository<LTwoInterest_LTwoInterestInput, ApplicationDbContext>
    {
        public LTwoInterest_LTwoInterestInputManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
