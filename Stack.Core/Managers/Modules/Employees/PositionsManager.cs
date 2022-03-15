using Stack.DAL;
using Stack.Repository;
using System.Collections.Generic;
using System.Text;
using Stack.Entities.Models.Modules.Employees;

namespace Stack.Core.Managers.Modules.Employees
{
    public class PositionsManager : Repository<Position, ApplicationDbContext>
    {
        public PositionsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }
}
