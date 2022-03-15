using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class GLAccountsManager : Repository<GLAccount, ApplicationDbContext>
    {
        public GLAccountsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
