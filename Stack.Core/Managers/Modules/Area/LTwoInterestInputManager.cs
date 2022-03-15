using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LTwoInterestInputManager : Repository<LTwoInterestInput, ApplicationDbContext>
    {
        public LTwoInterestInputManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
