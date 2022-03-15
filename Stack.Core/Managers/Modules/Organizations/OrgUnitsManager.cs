using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class OrgUnitsManager : Repository<OrgUnit, ApplicationDbContext>
    {
        public OrgUnitsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
