using Stack.DAL;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Core.Managers.Modules.pool
{
    public class PoolUserManager : Repository<Pool_Users, ApplicationDbContext>
    {
        public PoolUserManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
