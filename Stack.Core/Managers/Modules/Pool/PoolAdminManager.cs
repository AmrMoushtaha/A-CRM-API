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
    public class PoolAdminManager : Repository<Pool_Admin, ApplicationDbContext>
    {
        public DbSet<Pool_Admin> dbSet;
        public ApplicationDbContext context;
        public PoolAdminManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Pool_Admin>();
            context = _context;

        }

        //Include Related customer-stages count
        public async Task<List<PoolSidebarViewModel>> GetUserPools(string userID)
        {

            return await Task.Run(() =>
            {
                return context.Pool_Admins.Where(t => t.UserID == userID)
                    .Select(t => t.Pool)
                       .Select(p => new PoolSidebarViewModel
                       {
                           ID = p.ID,
                           NameEN = p.NameEN,
                           NameAR = p.NameEN,
                           ContactCount = p.Contacts.Where(t => t.Status.Status == ContactState.Unassigned.ToString()).Count()
                       }).ToList();
            });
        }

        public async Task<List<PoolSidebarViewModel>> GetPoolDetails(long poolID)
        {

            return await Task.Run(() =>
            {
                return context.Pool_Users.Where(t => t.PoolID == poolID)
                    .Select(t => t.Pool)
                       .Select(p => new PoolSidebarViewModel
                       {
                           ID = p.ID,
                           NameEN = p.NameEN,
                           NameAR = p.NameEN,
                           DescriptionAR = p.DescriptionAR,
                           DescriptionEN = p.DescriptionEN,
                       }).ToList();
            });
        }

        public async Task<List<ContactListViewModel>> GetPoolContacts(long poolID, string userID)
        {

            return await Task.Run(() =>
            {

                var enumerable = context.Pool_Admins.Where(t => t.PoolID == poolID && t.UserID == userID)
                            .Select(a => new
                            {
                                ID = a.PoolID,
                                filteredContacts = a.Pool.Contacts.Where(t => t.Status.Status == ContactState.Unassigned.ToString())
                            })
                            .SelectMany(a => a.
                            filteredContacts.Select(p => new ContactListViewModel
                            {
                                ID = p.ID,
                                FirstNameEN = p.FirstNameEN,
                                LastNameEN = p.LastNameEN,
                                FirstNameAR = p.FirstNameAR,
                                LastNameAR = p.LastNameAR,
                                PrimaryPhoneNumber = p.PrimaryPhoneNumber
                            })).ToList();

                //List<ContactListViewModel> asList = new List<ContactListViewModel>();

                //foreach (var item in enumerable)
                //{
                    
                //    asList.Add(item);
                //};

                var contacts = enumerable.ToList();
                if (contacts != null && contacts.Count > 0)
                {
                    return contacts;
                }
                else
                {
                    return null;
                }
            });
        }

    }

}
