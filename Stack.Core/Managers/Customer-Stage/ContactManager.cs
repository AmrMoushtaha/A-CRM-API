using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Materials
{
    public class ContactManager : Repository<Contact, ApplicationDbContext>
    {
        public DbSet<Contact> dbSet;
        public DbSet<ContactPhoneNumber> phoneNumberdbSet;
        public ApplicationDbContext context;

        public ContactManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Contact>();
            context = _context;
        }

        public async Task<ContactViewModel> GetContactDetails(long contactID)
        {

            return await Task.Run(() =>
            {
                var Contact = context.Contacts.Where(t => t.ID == contactID)
                   .Select(p => new ContactViewModel
                   {
                       ID = p.ID,
                       FullNameAR = p.FullNameAR,
                       FullNameEN = p.FullNameEN,
                       Address = p.Address,
                       Email = p.Email,
                       Channel = context.Channels.Where(t => t.ID == p.ChannelID).Select(c => new RecordChannelViewModel
                       {
                           ID = c.ID,
                           TitleEN = c.TitleEN,
                           TitleAR = c.TitleAR,
                           DescriptionEN = c.DescriptionEN,
                           DescriptionAR = c.DescriptionAR,
                       }).FirstOrDefault(),
                       LST = context.LeadSourceTypes.Where(t => t.ID == p.LSTID).Select(c => new RecordChannelViewModel
                       {
                           ID = c.ID,
                           TitleEN = c.TitleEN,
                           TitleAR = c.TitleAR,
                           DescriptionEN = c.DescriptionEN,
                           DescriptionAR = c.DescriptionAR,
                       }).FirstOrDefault(),
                       LSN = context.LeadSourceNames.Where(t => t.ID == p.LSNID).Select(c => new RecordChannelViewModel
                       {
                           ID = c.ID,
                           TitleEN = c.TitleEN,
                           TitleAR = c.TitleAR,
                           DescriptionEN = c.DescriptionEN,
                           DescriptionAR = c.DescriptionAR,
                       }).FirstOrDefault(),
                       Occupation = p.Occupation,
                       PrimaryPhoneNumber = p.PrimaryPhoneNumber,
                       StatusEN = p.Status.EN,
                       StatusAR = p.Status.AR,
                       StatusID = p.Status.ID,
                       AssignedUserName = p.AssignedUser.FirstName + " " + p.AssignedUser.LastName,
                   }).FirstOrDefault();

                if (Contact != null)
                {
                    var phoneNumbers = context.ContactPhoneNumbers.Where(t => t.ContactID == contactID)
                    .Select(t => new ContactPhoneNumberDTO
                    {
                        ID = t.ID,
                        Number = t.Number,
                    }).ToList();

                    Contact.ContactPhoneNumbers = phoneNumbers;

                    var comments = context.ContactComments.Where(t => t.ContactID == contactID)
                    .Select(t => new ContactCommentDTO
                    {
                        Comment = t.Comment,
                        CreatedBy = t.CreatedBy,
                        CreationDate = t.CreationDate
                    }).ToList();

                    Contact.Comments = comments;

                    var tags = context.Contact_Tags.Where(t => t.ContactID == contactID)
                    .Select(t => new ContactTagDTO
                    {
                        ID = t.TagID,
                        title = t.Tag.Title

                    }).ToList();

                    Contact.Tags = tags;

                    return Contact;
                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<List<ContactListViewModel>> GetAssignedContacts(string userID)
        {
            return await Task.Run(() =>
            {
                return context.Contacts.Where(t => t.AssignedUserID == userID && t.State == (int)CustomerStageState.Initial)
                   .Select(p => new ContactListViewModel
                   {
                       ID = p.ID,
                       FullNameEN = p.FullNameEN,
                       FullNameAR = p.FullNameAR,
                       PrimaryPhoneNumber = p.PrimaryPhoneNumber,
                   }).ToList();

            });
        }

        public async Task<List<ContactListViewModel>> GetAssignedFreshContacts(string userID)
        {
            return await Task.Run(() =>
            {
                return context.Contacts.Where(t => t.AssignedUserID == userID && t.State == (int)CustomerStageState.Initial && t.IsFresh == true)
                   .Select(p => new ContactListViewModel
                   {
                       ID = p.ID,
                       FullNameEN = p.FullNameEN,
                       FullNameAR = p.FullNameAR,
                       PrimaryPhoneNumber = p.PrimaryPhoneNumber,
                   }).ToList();

            });
        }
    }

}
