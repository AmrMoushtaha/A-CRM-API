using AutoMapper;
using Stack.DTOs.Models.Modules.CR;
using Stack.Entities.Models.Modules.CR;

namespace Stack.API.AutoMapperConfig
{
    public class CustomerRequestMapperProfile : Profile
    {
        public CustomerRequestMapperProfile()
        {
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
