using Stack.DAL;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Hierarchy
{
    public class AttributesManager : Repository<LAttribute, ApplicationDbContext>
    {
        public AttributesManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
