using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.CustomerRequest;
using Stack.Repository;


namespace Stack.Core.Managers.Modules.CR
{
    public class CustomerRequestsManager : Repository<CustomerRequest, ApplicationDbContext>
    {
        public CustomerRequestsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
