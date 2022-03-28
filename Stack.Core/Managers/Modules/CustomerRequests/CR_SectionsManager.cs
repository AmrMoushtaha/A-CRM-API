using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.CustomerRequest;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.CR
{
    public class CR_SectionsManager : Repository<CR_Section, ApplicationDbContext>
    {
        public CR_SectionsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
