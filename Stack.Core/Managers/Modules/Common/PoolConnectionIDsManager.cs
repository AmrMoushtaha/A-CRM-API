using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Common;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Common
{
    public class PoolConnectionIDsManager : Repository<PoolConnectionID, ApplicationDbContext>
    {
        public DbSet<PoolConnectionID> dbSet;
        public ApplicationDbContext context;

        public PoolConnectionIDsManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<PoolConnectionID>();
            context = _context;
        }

        //public async Task<int> ClearAllConnections()
        //{
        //    return await Task.Run(() =>
        //    {
        //        return context.Database.ExecuteSqlRaw("TRUNCATE TABLE [PoolConnectionIDs]");
        //    });
        //}

    }

}
