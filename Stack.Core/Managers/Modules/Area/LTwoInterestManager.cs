using Stack.DAL;
using Stack.Entities.Models.Modules.AreaInterest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LTwoInterestManager : Repository<LTwoInterest, ApplicationDbContext>
    {
        public LTwoInterestManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
