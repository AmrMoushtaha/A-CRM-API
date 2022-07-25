using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.Pool;
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
    public class TeamUserManager : Repository<Team_User, ApplicationDbContext>
    {
        public DbSet<Team_User> dbSet;
        public ApplicationDbContext context;
        public TeamUserManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Team_User>();
            context = _context;
        }

        public async Task<List<TeamMemberViewModel>> GetTeamMembers(long teamID)
        {

            return await Task.Run(() =>
            {
                return context.Team_Users
                .Where(t => t.TeamID == teamID)
                       .Select(p => new TeamMemberViewModel
                       {
                           UserID = p.UserID,
                           FullName = p.User.FirstName + " " + p.User.LastName,
                           IsManager = p.IsManager,
                           ManagerID = p.ManagerID,
                           JoinDate = p.JoinDate,
                           Status = p.Status,
                       }).ToList();
            });
        }

        public async Task<List<TeamMemberViewModel>> GetSubordinatesContacts(string managerID, int state)
        {
            return await Task.Run(() =>
            {
                return context.Team_Users
                .Where(t => t.ManagerID == managerID)
                       .Select(p => new TeamMemberViewModel
                       {
                           UserID = p.UserID,
                           FullName = p.User.FirstName + " " + p.User.LastName,
                           IsManager = p.IsManager,
                           ManagerID = p.ManagerID,
                           JoinDate = p.JoinDate,
                           Status = p.Status,
                           RecordsList = context.Contacts.Where(t => t.AssignedUserID == p.UserID && t.State == state).Select(
                               z => new ContactListViewModel
                               {
                                   ID = z.ID,
                                   FullNameEN = z.FullNameEN,
                                   FullNameAR = z.FullNameAR,
                                   PrimaryPhoneNumber = z.PrimaryPhoneNumber
                               }).ToList()
                       }).ToList();



            });
        }

        public async Task<List<TeamMemberViewModel>> GetSubordinatesProspects(string managerID, int state)
        {
            return await Task.Run(() =>
            {
                return context.Team_Users
                .Where(t => t.ManagerID == managerID)
                       .Select(p => new TeamMemberViewModel
                       {
                           UserID = p.UserID,
                           FullName = p.User.FirstName + " " + p.User.LastName,
                           IsManager = p.IsManager,
                           ManagerID = p.ManagerID,
                           JoinDate = p.JoinDate,
                           Status = p.Status,
                           RecordsList = context.Prospects.Where(t => t.AssignedUserID == p.UserID && t.State == state).Select(
                               z => new ContactListViewModel
                               {
                                   ID = z.ID,
                                   FullNameEN = z.Deal.Customer.FullNameEN,
                                   FullNameAR = z.Deal.Customer.FullNameEN,
                                   PrimaryPhoneNumber = z.Deal.Customer.PrimaryPhoneNumber
                               }).ToList()
                       }).ToList();
            });
        }

        public async Task<List<TeamMemberViewModel>> GetSubordinatesLeads(string managerID, int state)
        {
            return await Task.Run(() =>
            {
                return context.Team_Users
                .Where(t => t.ManagerID == managerID)
                       .Select(p => new TeamMemberViewModel
                       {
                           UserID = p.UserID,
                           FullName = p.User.FirstName + " " + p.User.LastName,
                           IsManager = p.IsManager,
                           ManagerID = p.ManagerID,
                           JoinDate = p.JoinDate,
                           Status = p.Status,
                           RecordsList = context.Prospects.Where(t => t.AssignedUserID == p.UserID && t.State == state).Select(
                               z => new ContactListViewModel
                               {
                                   ID = z.ID,
                                   FullNameEN = z.Deal.Customer.FullNameEN,
                                   FullNameAR = z.Deal.Customer.FullNameEN,
                                   PrimaryPhoneNumber = z.Deal.Customer.PrimaryPhoneNumber
                               }).ToList()
                       }).ToList();
            });
        }

        public async Task<List<TeamMemberViewModel>> GetSubordinatesOpportunities(string managerID, int state)
        {
            return await Task.Run(() =>
            {
                return context.Team_Users
                .Where(t => t.ManagerID == managerID)
                       .Select(p => new TeamMemberViewModel
                       {
                           UserID = p.UserID,
                           FullName = p.User.FirstName + " " + p.User.LastName,
                           IsManager = p.IsManager,
                           ManagerID = p.ManagerID,
                           JoinDate = p.JoinDate,
                           Status = p.Status,
                           RecordsList = context.Prospects.Where(t => t.AssignedUserID == p.UserID && t.State == state).Select(
                               z => new ContactListViewModel
                               {
                                   ID = z.ID,
                                   FullNameEN = z.Deal.Customer.FullNameEN,
                                   FullNameAR = z.Deal.Customer.FullNameEN,
                                   PrimaryPhoneNumber = z.Deal.Customer.PrimaryPhoneNumber
                               }).ToList()
                       }).ToList();
            });
        }

        public async Task<List<TeamMemberViewModel>> GetSubordinatesDoneDeals(string managerID, int state)
        {
            return await Task.Run(() =>
            {
                return context.Team_Users
                .Where(t => t.ManagerID == managerID)
                       .Select(p => new TeamMemberViewModel
                       {
                           UserID = p.UserID,
                           FullName = p.User.FirstName + " " + p.User.LastName,
                           IsManager = p.IsManager,
                           ManagerID = p.ManagerID,
                           JoinDate = p.JoinDate,
                           Status = p.Status,
                           RecordsList = context.Prospects.Where(t => t.AssignedUserID == p.UserID && t.State == state).Select(
                               z => new ContactListViewModel
                               {
                                   ID = z.ID,
                                   FullNameEN = z.Deal.Customer.FullNameEN,
                                   FullNameAR = z.Deal.Customer.FullNameEN,
                                   PrimaryPhoneNumber = z.Deal.Customer.PrimaryPhoneNumber
                               }).ToList()
                       }).ToList();
            });
        }

    }

}
