using Stack.DAL;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class Area_PoolManager : Repository<Area_Pool, ApplicationDbContext>
    {
        public Area_PoolManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
