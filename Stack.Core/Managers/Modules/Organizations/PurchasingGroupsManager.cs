using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class PurchasingGroupsManager : Repository<PurchasingGroup, ApplicationDbContext>
    {
        public PurchasingGroupsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
