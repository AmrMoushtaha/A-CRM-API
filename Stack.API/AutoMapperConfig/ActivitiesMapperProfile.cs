using AutoMapper;
using Stack.DTOs.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Activities;

namespace Stack.API.AutoMapperConfig
{
    public class ActivitiesMapperProfile : Profile
    {
        public ActivitiesMapperProfile()
        {

            CreateMap<ActivityType, ActivityTypeMainViewDTO>()
            .ReverseMap();

            CreateMap<Activity, ActivityHistoryViewDTO>()
            .ForMember(dest => dest.ActivityTypeNameAR, m => m.MapFrom(d => d.ActivityType.NameAR))
            .ForMember(dest => dest.ActivityTypeNameEN, m => m.MapFrom(d => d.ActivityType.NameEN))
            .ForMember(dest => dest.SubmissionDate, m => m.MapFrom(d => d.SubmissionDate))
            .ForMember(dest => dest.ColorCode, m => m.MapFrom(d => d.ActivityType.ColorCode))
            .ForMember(dest => dest.CreatedBy, m => m.MapFrom(d => d.ApplicationUser.FirstName + " " + d.ApplicationUser.LastName))
            .ReverseMap();

        }

    }

}
