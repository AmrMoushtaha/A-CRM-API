using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class SectionsManager : Repository<Section, ApplicationDbContext>
    {
        public SectionsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
