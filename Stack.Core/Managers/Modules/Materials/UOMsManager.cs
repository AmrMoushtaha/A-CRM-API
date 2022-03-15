using Stack.DAL;
using Stack.Entities.Models.Modules.Materials;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Materials
{
    public class UOMsManager : Repository<UOM, ApplicationDbContext>
    {
        public UOMsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
