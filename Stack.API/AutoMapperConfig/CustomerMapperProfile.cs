using AutoMapper;
using Stack.DTOs.Models.Modules.AreaInterest;
using Stack.DTOs.Models.Modules.Areas;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Models.Modules.CR;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.Channel;
using Stack.DTOs.Requests.Modules.Channels;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Hierarchy;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.Entities.Enums.Modules.Pool;
using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Channel;
using Stack.Entities.Models.Modules.Channels;
using Stack.Entities.Models.Modules.CR;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Entities.Models.Modules.Interest;
using System.Linq;

namespace Stack.API.AutoMapperConfig
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {

            CreateMap<ContactPhoneNumber, ContactPhoneNumberDTO>()
            .ReverseMap();

            CreateMap<ContactComment, ContactCommentDTO>()
           .ReverseMap();

            CreateMap<CustomerComment, ContactCommentDTO>()
           .ReverseMap();


            CreateMap<ContactComment, CommentReponseModel>()
           .ReverseMap();

            CreateMap<CustomerComment, CommentReponseModel>()
           .ReverseMap();

            CreateMap<Contact_Tag, ContactTagDTO>()
           .ReverseMap();

            CreateMap<Customer_Tag, ContactTagDTO>()
           .ReverseMap();

            CreateMap<Tag, TagsDTO>()
           .ReverseMap();

            CreateMap<ContactStatus, ContactStatusViewModel>()
           .ReverseMap();

            CreateMap<Deal, RecordDeal>()
            .ForMember(dist => dist.RecordID, opt => opt.MapFrom(t => t.ActiveStageID))
            .ForMember(dist => dist.RecordType, opt => opt.MapFrom(t => t.ActiveStageType))
            .ReverseMap();

            CreateMap<Contact, ContactViewModel>()
           .ForMember(dist => dist.ContactPhoneNumbers, opt => opt.MapFrom(t => t.PhoneNumbers))
           .ForMember(dist => dist.AssignedUserName, opt => opt.MapFrom(t => t.AssignedUser.FirstName + " " + t.AssignedUser.LastName))
           .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Comments))
           .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.EN))
           .ForMember(dist => dist.StatusAR, opt => opt.MapFrom(t => t.Status.AR))
           .ForMember(dist => dist.StatusID, opt => opt.MapFrom(t => t.Status.ID))
           .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Tags))
           .ReverseMap();

            CreateMap<Contact, ContactListViewModel>()
            .ReverseMap();

            CreateMap<Prospect, ContactListViewModel>()
            .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
            .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
            .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
            .ReverseMap();

            CreateMap<Lead, ContactListViewModel>()
           .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
           .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
           .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
           .ReverseMap();

            CreateMap<Opportunity, ContactListViewModel>()
          .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
          .ReverseMap();

            CreateMap<DoneDeal, ContactListViewModel>()
          .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
          .ReverseMap();

            CreateMap<Contact_Favorite, ContactListViewModel>()
           .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Contact.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Contact.FullNameEN))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Contact.PrimaryPhoneNumber))
          .ForMember(dist => dist.ID, opt => opt.MapFrom(t => t.Contact.ID))
          .ForMember(dist => dist.IsLocked, opt => opt.MapFrom(t => t.Contact.IsLocked))
           .ReverseMap();

            CreateMap<Prospect_Favorite, ContactListViewModel>()
           .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Record.Deal.Customer.PrimaryPhoneNumber))
          .ForMember(dist => dist.ID, opt => opt.MapFrom(t => t.Record.ID))
          .ForMember(dist => dist.IsLocked, opt => opt.MapFrom(t => t.Record.IsLocked))
           .ReverseMap();

            CreateMap<Lead_Favorite, ContactListViewModel>()
           .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Record.Deal.Customer.PrimaryPhoneNumber))
          .ForMember(dist => dist.ID, opt => opt.MapFrom(t => t.Record.ID))
          .ForMember(dist => dist.IsLocked, opt => opt.MapFrom(t => t.Record.IsLocked))
           .ReverseMap();

            CreateMap<Opportunity_Favorite, ContactListViewModel>()
           .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Record.Deal.Customer.PrimaryPhoneNumber))
          .ForMember(dist => dist.ID, opt => opt.MapFrom(t => t.Record.ID))
          .ForMember(dist => dist.IsLocked, opt => opt.MapFrom(t => t.Record.IsLocked))
           .ReverseMap();

            CreateMap<DoneDeal_Favorite, ContactListViewModel>()
           .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Record.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Record.Deal.Customer.PrimaryPhoneNumber))
          .ForMember(dist => dist.ID, opt => opt.MapFrom(t => t.Record.ID))
           .ReverseMap();



            //Single View
            CreateMap<Prospect, ContactViewModel>()
            .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.EN))
            .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.AR))
            .ForMember(dist => dist.Email, opt => opt.MapFrom(t => t.Deal.Customer.Email))
            .ForMember(dist => dist.Address, opt => opt.MapFrom(t => t.Deal.Customer.Address))
            .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
            .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
            .ForMember(dist => dist.Occupation, opt => opt.MapFrom(t => t.Deal.Customer.Occupation))
            .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
            //.ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
            //.ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
            //.ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
            .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Comments))
            .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
            .ForMember(dist => dist.CustomerID, opt => opt.MapFrom(t => t.Deal.Customer.ID))
            .ReverseMap();

            CreateMap<Lead, ContactViewModel>()
              .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.EN))
              .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.AR))
              .ForMember(dist => dist.Email, opt => opt.MapFrom(t => t.Deal.Customer.Email))
              .ForMember(dist => dist.Address, opt => opt.MapFrom(t => t.Deal.Customer.Address))
              .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
              .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
              .ForMember(dist => dist.Occupation, opt => opt.MapFrom(t => t.Deal.Customer.Occupation))
              .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
              //.ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
              //.ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
              //.ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
              .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Comments))
              .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
              .ForMember(dist => dist.CustomerID, opt => opt.MapFrom(t => t.Deal.Customer.ID))
              .ReverseMap();

            CreateMap<Opportunity, ContactViewModel>()
              .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.EN))
              .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.AR))
              .ForMember(dist => dist.Email, opt => opt.MapFrom(t => t.Deal.Customer.Email))
              .ForMember(dist => dist.Address, opt => opt.MapFrom(t => t.Deal.Customer.Address))
              .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
              .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
              .ForMember(dist => dist.Occupation, opt => opt.MapFrom(t => t.Deal.Customer.Occupation))
              .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
              //.ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
              //.ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
              //.ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
              .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Comments))
              .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
              .ForMember(dist => dist.CustomerID, opt => opt.MapFrom(t => t.Deal.Customer.ID))
              .ReverseMap();

            CreateMap<DoneDeal, ContactViewModel>()
              .ForMember(dist => dist.Email, opt => opt.MapFrom(t => t.Deal.Customer.Email))
              .ForMember(dist => dist.Address, opt => opt.MapFrom(t => t.Deal.Customer.Address))
              .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
              .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
              .ForMember(dist => dist.Occupation, opt => opt.MapFrom(t => t.Deal.Customer.Occupation))
              .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
              //.ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
              //.ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
              //.ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
              .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Comments))
              .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
              .ForMember(dist => dist.CustomerID, opt => opt.MapFrom(t => t.Deal.Customer.ID))
              .ReverseMap();



        }

    }

}
