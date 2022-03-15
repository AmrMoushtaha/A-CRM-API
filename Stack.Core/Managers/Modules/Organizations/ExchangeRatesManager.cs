using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class ExchangeRatesManager : Repository<ExchangeRate, ApplicationDbContext>
    {
        public ExchangeRatesManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
