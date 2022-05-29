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
    public class CR_TimelineManager : Repository<CR_Timeline, ApplicationDbContext>
    {
        public DbSet<CR_Timeline> dbSet;
        public ApplicationDbContext context;

        public CR_TimelineManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<CR_Timeline>();
            context = _context;
        }

    }

}
