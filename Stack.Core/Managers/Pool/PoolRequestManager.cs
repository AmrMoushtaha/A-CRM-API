using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Pools
{
    public class PoolRequestManager : Repository<PoolRequest, ApplicationDbContext>
    {
        public DbSet<PoolRequest> dbSet;
        public ApplicationDbContext context;
        public PoolRequestManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<PoolRequest>();
            context = _context;
        }



    }

}
