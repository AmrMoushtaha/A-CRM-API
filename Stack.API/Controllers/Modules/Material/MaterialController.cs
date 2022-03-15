using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Materials.MaterialGroups;
using Stack.DTOs.Requests.Modules.Materials;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Materials;
using System.Threading.Tasks;
using Stack.DTOs.Requests.Modules.Materials.Material;
using Stack.DTOs.Requests.Modules.Materials.MaterialTypes;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/Materials")]
    [ApiController]
  
    public class MaterialController : BaseResultHandlerController<MaterialService>
    {
        public MaterialController(MaterialService _service) : base(_service)
        {

        }

     
        [HttpGet("GetAllMaterials")]
        public async Task<IActionResult> GetAllMaterial()
        {
            return await GetResponseHandler(async () => await service.GetAllMaterials());
        }

        [HttpPost("CreateMaterial")]
        public async Task<IActionResult> CreateMaterial(MaterialCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateMaterial(model));
        }


        [HttpPost("EditMaterial")]
        public async Task<IActionResult> EditMaterial(EditMaterialModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditMaterial(model));
        }

        [HttpPost("DeleteMaterial")]
        public async Task<IActionResult> DeleteMaterial(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteMaterial(model));
        }



    }

}
