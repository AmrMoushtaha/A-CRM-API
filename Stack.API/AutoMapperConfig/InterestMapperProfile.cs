using AutoMapper;
using Stack.DTOs.Models.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.Hierarchy;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.Entities.Models.Modules.Hierarchy;
using Stack.Entities.Models.Modules.Interest;

namespace Stack.API.AutoMapperConfig
{
    public class InterestMapperProfile : Profile
    {
        public InterestMapperProfile()
        {
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


            CreateMap<Input, InputToAdd>()
           .ReverseMap();
            CreateMap<Input, InputToEdit>()
           .ReverseMap();
        }

    }

}
