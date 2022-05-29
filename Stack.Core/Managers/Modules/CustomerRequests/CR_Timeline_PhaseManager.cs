﻿using Microsoft.EntityFrameworkCore;
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
    public class CR_Timeline_PhaseManager : Repository<CR_Timeline_Phase, ApplicationDbContext>
    {
        public DbSet<CR_Timeline_Phase> dbSet;
        public ApplicationDbContext context;

        public CR_Timeline_PhaseManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<CR_Timeline_Phase>();
            context = _context;
        }

    }

}
