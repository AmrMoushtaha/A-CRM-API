using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Models.Modules.Teams;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Teams;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Teams
{
    public class TeamManager : Repository<Team, ApplicationDbContext>
    {
        public DbSet<Team> dbSet;
        public ApplicationDbContext context;
        public TeamManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Team>();
            context = _context;
        }


        public async Task<List<TeamSidebarViewModel>> GetAllTeams()
        {

            return await Task.Run(() =>
            {
                return context.Teams
                       .Select(p => new TeamSidebarViewModel
                       {
                           ID = p.ID,
                           NameEN = p.NameEN,
                           NameAR = p.NameAR,
                           DescriptionEN = p.DescriptionEN,
                           DescriptionAR = p.DescriptionAR,
                           MembersCount = p.Team_Users.Count()
                       }).ToList();
            });
        }

    }

}
