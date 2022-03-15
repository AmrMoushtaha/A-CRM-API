using Stack.DAL;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class AreaManager : Repository<Area, ApplicationDbContext>
    {
        public AreaManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
