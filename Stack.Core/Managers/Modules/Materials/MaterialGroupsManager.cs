using Stack.DAL;
using Stack.Entities.Models.Modules.Materials;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Materials
{
    public class MaterialGroupsManager : Repository<MaterialGroup, ApplicationDbContext>
    {
        public MaterialGroupsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
