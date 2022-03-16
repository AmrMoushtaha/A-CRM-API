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

namespace Stack.Core.Managers.Modules.pool
{
    public class PoolManager : Repository<Pool, ApplicationDbContext>
    {
        public DbSet<Pool> dbSet;
        public ApplicationDbContext context;
        public PoolManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Pool>();
            context = _context;
        }



        public async Task<PoolSidebarViewModel> GetPoolDetails(long poolID)
        {

            return await Task.Run(() =>
            {
                return context.Pools.Where(t => t.ID == poolID)
                   .Select(p => new PoolSidebarViewModel
                   {
                       ID = p.ID,
                       NameEN = p.NameEN,
                       NameAR = p.NameEN,
                       DescriptionAR = p.DescriptionAR,
                       DescriptionEN = p.DescriptionEN,
                   }).FirstOrDefault();
            });
        }

    }

}
