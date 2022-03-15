using Stack.DTOs.Models.Modules.Materials.MaterialTypes;
using Stack.Entities.Models.Modules.Materials;

namespace Stack.API.AutoMapperConfig
{
    public class MaterialTypesMapper : AutoMapperProfile
    {
        public MaterialTypesMapper()
        {

           CreateMap<MaterialType, MaterialTypesMainViewDTO>()
          .ReverseMap();

        }

    }

}
