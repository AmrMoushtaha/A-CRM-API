using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class ProfitCentersManager : Repository<ProfitCenter, ApplicationDbContext>
    {
        public ProfitCentersManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
