using Stack.DAL;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Hierarchy
{
    public class LevelManager : Repository<Level, ApplicationDbContext>
    {
        public LevelManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
