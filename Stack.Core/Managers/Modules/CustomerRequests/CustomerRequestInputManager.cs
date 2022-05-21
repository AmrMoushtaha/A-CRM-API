using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.CR;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.CR
{
    public class CustomerRequestInputManager : Repository<CustomerRequestInput, ApplicationDbContext>
    {
        public DbSet<CustomerRequestInput> dbSet;
        public ApplicationDbContext context;

        public CustomerRequestInputManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<CustomerRequestInput>();
            context = _context;
        }

    }

}
