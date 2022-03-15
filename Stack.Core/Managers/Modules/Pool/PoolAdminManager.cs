using Stack.DAL;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Core.Managers.Modules.pool
{
    public class PoolAdminManager : Repository<Pool_Admin, ApplicationDbContext>
    {
        public PoolAdminManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
