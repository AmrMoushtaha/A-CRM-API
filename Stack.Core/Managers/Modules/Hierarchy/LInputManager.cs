using Stack.DAL;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Hierarchy
{
    public class LInputManager : Repository<Input, ApplicationDbContext>
    {
        public LInputManager(ApplicationDbContext _context) : base(_context)
        {

            dbSet = _context.Set<Input>();
            context = _context;

        }

        public async Task<int> MaxOrder()
        {
            return await Task.Run(() =>
            {
                return context.Inputs.Max(o => o.Order);
            });
        }
    }

}


