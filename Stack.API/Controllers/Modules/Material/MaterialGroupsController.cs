using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Materials.MaterialGroups;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Materials;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/MaterialGroups")]
    [ApiController]
  
    public class MaterialGroupsController : BaseResultHandlerController<MaterialGroupsService>
    {
        public MaterialGroupsController(MaterialGroupsService _service) : base(_service)
        {

        }

     
        [HttpGet("GetAllMaterialGroups")]
        public async Task<IActionResult> GetAllMaterialGroups()
        {
            return await GetResponseHandler(async () => await service.GetAllMaterialGroups());
        }

        [HttpPost("CreateMaterialGroup")]
        public async Task<IActionResult> CreateMaterialGroup(MaterialTypesCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateMaterialGroup(model));
        }


        [HttpPost("EditMaterialGroup")]
        public async Task<IActionResult> EditPlant(EditMaterialGroupModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditMaterialGroup(model));
        }

        [HttpPost("DeleteMaterialGroup")]
        public async Task<IActionResult> DeletePlant(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteMaterialGroup(model));
        }



    }

}
