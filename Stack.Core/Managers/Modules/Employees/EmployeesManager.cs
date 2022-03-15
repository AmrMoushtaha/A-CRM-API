using Stack.DAL;
using Stack.Entities.Models.Modules.Employees;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Core.Managers.Modules.Employees
{
    public class EmployeesManager : Repository<Employee, ApplicationDbContext>
    {
        public EmployeesManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }
}
