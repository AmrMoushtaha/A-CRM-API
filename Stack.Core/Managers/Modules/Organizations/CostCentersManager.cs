using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class CostCentersManager : Repository<CostCenter, ApplicationDbContext>
    {
        public CostCentersManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
