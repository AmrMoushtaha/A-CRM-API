using Stack.DAL;
using Stack.Entities.Models.Modules.Areas;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.area
{
    public class LocationManager : Repository<Location, ApplicationDbContext>
    {
        public LocationManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
