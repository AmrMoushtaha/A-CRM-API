using Stack.DTOs.Models.Modules.Materials.MaterialTypes;
using Stack.Entities.Models.Modules.Materials;

namespace Stack.API.AutoMapperConfig
{
    public class MaterialsMapper : AutoMapperProfile
    {
        public MaterialsMapper()
        {

            CreateMap<Material, MaterialMainViewDTO>()
            .ForMember(dest => dest.MaterialGroupCode, opt => opt.MapFrom(e => e.MaterialGroup.Code))
            .ForMember(dest => dest.MaterialTypeCode, opt => opt.MapFrom(e => e.MaterialType.Code))
           .ReverseMap();

        }

    }

}
