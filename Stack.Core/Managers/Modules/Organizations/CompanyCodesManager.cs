using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class CompanyCodesManager : Repository<CompanyCode, ApplicationDbContext>
    {
        public CompanyCodesManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
