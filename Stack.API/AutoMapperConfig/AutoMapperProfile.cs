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
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>()
            .ReverseMap();

            CreateMap<ApplicationRole, ApplicationRoleDTO>()
            .ReverseMap();

            CreateMap<Pool_User, PoolSidebarViewModel>()
            .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.Pool.NameEN))
            .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.Pool.NameAR))
            .ReverseMap();

            CreateMap<Pool_User, PoolAssignedUsersModel>()
            .ForMember(dist => dist.UserID, opt => opt.MapFrom(t => t.UserID))
            .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.User.FirstName + " " + t.User.LastName))
            .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.User.FirstName + " " + t.User.LastName))
            .ReverseMap();

            CreateMap<Pool_User, PoolAssignedUserCapacityModel>()
            .ForMember(dist => dist.FullName, opt => opt.MapFrom(t => t.User.FirstName + " " + t.User.LastName))
            .ForMember(dist => dist.Capacity, opt => opt.MapFrom(t => t.Capacity))
            .ReverseMap();

            CreateMap<Pool, PoolConfigurationModel>()
            .ForMember(dist => dist.PoolID, opt => opt.MapFrom(t => t.ID))
            .ForMember(dist => dist.UsersCount, opt => opt.MapFrom(t => t.Pool_Users.Where(a => a.IsAdmin == false).Count()))
            .ForMember(dist => dist.AdminsCount, opt => opt.MapFrom(t => t.Pool_Users.Where(a => a.IsAdmin == true).Count()))
            .ForMember(dist => dist.RequestsCount, opt => opt.MapFrom(t => t.Requests.Where(a => a.Status == (int)PoolRequestStatuses.Pending).Count()))
            .ReverseMap();

            CreateMap<PoolRequest, PoolRequestModel>()
            .ForMember(dist => dist.PoolID, opt => opt.MapFrom(t => t.ID))
            .ForMember(dist => dist.PoolNameAR, opt => opt.MapFrom(t => t.Pool.NameAR))
            .ForMember(dist => dist.PoolNameEN, opt => opt.MapFrom(t => t.Pool.NameEN))
            .ReverseMap();

            CreateMap<Deal, RecordDeal>()
            .ForMember(dist => dist.RecordID, opt => opt.MapFrom(t => t.ActiveStageID))
            .ForMember(dist => dist.RecordType, opt => opt.MapFrom(t => t.ActiveStageType))
            .ReverseMap();

            //Contact
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

            CreateMap<Prospect, ContactViewModel>()
          .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.EN))
          .ForMember(dist => dist.StatusEN, opt => opt.MapFrom(t => t.Status.AR))
          .ForMember(dist => dist.Email, opt => opt.MapFrom(t => t.Deal.Customer.Email))
          .ForMember(dist => dist.Address, opt => opt.MapFrom(t => t.Deal.Customer.Address))
          .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.Occupation, opt => opt.MapFrom(t => t.Deal.Customer.Occupation))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
          .ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
          .ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
          .ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
          .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Comments))
          .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
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
          .ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
          .ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
          .ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
          .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Comments))
          .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
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
          .ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
          .ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
          .ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
          .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Comments))
          .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
          .ReverseMap();

            CreateMap<DoneDeal, ContactViewModel>()
          .ForMember(dist => dist.Email, opt => opt.MapFrom(t => t.Deal.Customer.Email))
          .ForMember(dist => dist.Address, opt => opt.MapFrom(t => t.Deal.Customer.Address))
          .ForMember(dist => dist.FullNameAR, opt => opt.MapFrom(t => t.Deal.Customer.FullNameAR))
          .ForMember(dist => dist.FullNameEN, opt => opt.MapFrom(t => t.Deal.Customer.FullNameEN))
          .ForMember(dist => dist.Occupation, opt => opt.MapFrom(t => t.Deal.Customer.Occupation))
          .ForMember(dist => dist.PrimaryPhoneNumber, opt => opt.MapFrom(t => t.Deal.Customer.PrimaryPhoneNumber))
          .ForMember(dist => dist.ChannelID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.ChannelID))
          .ForMember(dist => dist.LSTID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSTID))
          .ForMember(dist => dist.LSNID, opt => opt.MapFrom(t => t.Deal.Customer.Contact.LSNID))
          .ForMember(dist => dist.Comments, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Comments))
          .ForMember(dist => dist.Tags, opt => opt.MapFrom(t => t.Deal.Customer.Contact.Tags))
          .ReverseMap();

            CreateMap<ContactPhoneNumber, ContactPhoneNumberDTO>()
           .ReverseMap();

            CreateMap<ContactComment, ContactCommentDTO>()
           .ReverseMap();


            CreateMap<ContactComment, CommentReponseModel>()
           .ReverseMap();

            CreateMap<Contact_Tag, ContactTagDTO>()
           .ReverseMap();


            CreateMap<Tag, TagsDTO>()
           .ReverseMap();

            CreateMap<ContactStatus, ContactStatusViewModel>()
           .ReverseMap();


            //Interest.
            CreateMap<LInterest, LInterestToAdd>()
             .ReverseMap();
            CreateMap<LInterest, LInterestToEdit>()
             .ReverseMap();

            CreateMap<LInterest, LInterestModel>()
            .ForMember(dist => dist.ParentLInterest, opt => opt.Ignore())
            .ForMember(dist => dist.Location, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<LInterestInput, LInterestInputModel>()
            .ReverseMap();

            CreateMap<LInterestInput, LInterestInputToAdd>()
            .ReverseMap();
            CreateMap<LInterestInput, LInterestInputToEdit>()
            .ReverseMap();
            CreateMap<LInterestInputToAdd, LInterestInputsToEdit>()
           .ReverseMap();

            CreateMap<Level, LevelToAdd>()
            .ReverseMap();
            CreateMap<Level, LevelToEdit>()
            .ReverseMap();

            CreateMap<LSection, SectionToAdd>()
            .ReverseMap();
            CreateMap<LSection, SectionToEdit>()
            .ReverseMap();


            CreateMap<Input, InputToAdd>()
            .ReverseMap();
            CreateMap<Input, InputToEdit>()
            .ReverseMap();

            CreateMap<LAttribute, AttributeToAdd>()
            .ReverseMap();
            CreateMap<LAttribute, AttributeToEdit>()
            .ReverseMap();

            CreateMap<Location, LocationModel>()
            .ForMember(dist => dist.ParentLocation, opt => opt.Ignore())
            .ReverseMap();
            CreateMap<Location, LocationToAdd>()
            .ReverseMap();
            CreateMap<Location, LocationToEdit>()
            .ReverseMap();

            CreateMap<Input, InputToAdd>()
           .ReverseMap();
            CreateMap<Input, InputToEdit>()
           .ReverseMap();

            CreateMap<Channel, ChannelViewModel>()
            .ReverseMap();
            CreateMap<LeadSourceType, LeadSourceTypeViewModel>()
            .ReverseMap();
            CreateMap<LeadSourceName, LeadSourceNameViewModel>()
            .ReverseMap();

            CreateMap<CRPhase, PhaseViewModel>()
            .ForMember(dist => dist.TitleAR, opt => opt.MapFrom(t => t.TitleAR))
            .ForMember(dist => dist.TitleEN, opt => opt.MapFrom(t => t.TitleEN))
            .ReverseMap();

            CreateMap<CRPhaseInput, PhaseInputViewModel>()
            .ForMember(dist => dist.Attributes, opt => opt.MapFrom(t => t.Options))
            .ForMember(dist => dist.Answers, opt => opt.MapFrom(t => t.Answers))
            .ReverseMap();

            CreateMap<CRPhaseInputAnswer, PhaseInputAnswerViewModel>()
            .ReverseMap();

            CreateMap<CRPhaseInputOption, PhaseAttributeViewModel>()
            .ForMember(dist => dist.LabelAR, opt => opt.MapFrom(t => t.TitleAR))
            .ForMember(dist => dist.LabelEN, opt => opt.MapFrom(t => t.TitleEN))
            .ReverseMap();

            CreateMap<CRTimeline, CRTimelineViewModel>()
            .ForMember(dist => dist.Phases, opt => opt.MapFrom(t => t.Phases))
            .ReverseMap();

            CreateMap<CR_Timeline, CRTimelineViewModel>()
            .ForMember(dist => dist.Phases, opt => opt.MapFrom(t => t.Timeline.Phases))
            .ForMember(dist => dist.TitleAR, opt => opt.MapFrom(t => t.Timeline.TitleAR))
            .ForMember(dist => dist.TitleEN, opt => opt.MapFrom(t => t.Timeline.TitleEN))
            .ReverseMap();


            CreateMap<CRTimeline_Phase, PhaseViewModel>()
            .ForMember(dist => dist.TitleAR, opt => opt.MapFrom(t => t.Phase.TitleAR))
            .ForMember(dist => dist.TitleEN, opt => opt.MapFrom(t => t.Phase.TitleEN))
            .ForMember(dist => dist.Inputs, opt => opt.MapFrom(t => t.Phase.Inputs))
            .ReverseMap();

            CreateMap<CustomerRequestType, CRTypeViewModel>()
            .ForMember(dist => dist.Timeline, opt => opt.MapFrom(t => t.PhasesTimeline))
            .ReverseMap();

            CreateMap<CustomerRequest, CRQuickViewModel>()
            .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.RequestType.NameAR))
            .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.RequestType.NameEN))
            .ForMember(dist => dist.Type, opt => opt.MapFrom(t => t.RequestType.Type))
            .ReverseMap();

            CreateMap<CustomerRequest, CRTypeViewModel>()
            .ForMember(dist => dist.NameAR, opt => opt.MapFrom(t => t.RequestType.NameAR))
            .ForMember(dist => dist.NameEN, opt => opt.MapFrom(t => t.RequestType.NameEN))
            .ForMember(dist => dist.Type, opt => opt.MapFrom(t => t.RequestType.Type))
            .ForMember(dist => dist.Timeline, opt => opt.MapFrom(t => t.Timeline[0]))
            .ReverseMap();
        }

    }

}
