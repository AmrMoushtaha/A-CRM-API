using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Materials.MaterialGroups;
using Stack.DTOs.Requests.Modules.Materials.MaterialTypes;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Materials;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/MaterialTypes")]
    [ApiController]
  
    public class MaterialTypesController : BaseResultHandlerController<MaterialTypesService>
    {
        public MaterialTypesController(MaterialTypesService _service) : base(_service)
        {

        }

     
        [HttpGet("GetAllMaterialTypes")]
        public async Task<IActionResult> GetAllMaterialTypes()
        {
            return await GetResponseHandler(async () => await service.GetAllMaterialTypes());
        }

        [HttpPost("CreateMaterialType")]
        public async Task<IActionResult> CreateMaterialType(MaterialTypesCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateMaterialType(model));
        }


        [HttpPost("EditMaterialType")]
        public async Task<IActionResult> EditMaterialType(EditMaterialTypeModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditMaterialType(model));
        }

        [HttpPost("DeleteMaterialType")]
        public async Task<IActionResult> DeleteMaterialType(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteMaterialType(model));
        }



    }

}
