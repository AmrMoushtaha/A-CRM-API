using Stack.DAL;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Hierarchy
{
    public class SectionManager : Repository<LSection, ApplicationDbContext>
    {
        public SectionManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
