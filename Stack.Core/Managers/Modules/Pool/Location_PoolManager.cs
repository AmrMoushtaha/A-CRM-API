using Stack.DAL;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Pools
{
    public class Location_PoolManager : Repository<Location_Pool, ApplicationDbContext>
    {
        public Location_PoolManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
