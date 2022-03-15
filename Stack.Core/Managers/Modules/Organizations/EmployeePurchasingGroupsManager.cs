using Stack.DAL;
using Stack.Entities.Models.Modules.Organizations;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Organizations
{
    public class EmployeePurchasingGroupsManager : Repository<Employee_PurchasingGroup, ApplicationDbContext>
    {
        public EmployeePurchasingGroupsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
