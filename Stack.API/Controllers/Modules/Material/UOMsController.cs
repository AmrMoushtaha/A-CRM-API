using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Materials.MaterialUOMs;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Materials;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/UOMs")]
    [ApiController]
    public class UOMsController : BaseResultHandlerController<UOMsService>
    {
        public UOMsController(UOMsService _service) : base(_service)
        {

        }

        [HttpGet("GetAllUOMs")]
        public async Task<IActionResult> GetAllUOMs()
        {
            return await GetResponseHandler(async () => await service.GetAllUOMs());
        }

        [HttpPost("CreateUOM")]
        public async Task<IActionResult> CreateUOM(MaterialUOMCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateUOM(model));
        }


        [HttpPost("EditUOM")]
        public async Task<IActionResult> EditUOM(EditMaterialUOMModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditUOM(model));
        }

        [HttpPost("DeleteUOM")]
        public async Task<IActionResult> DeleteUOM(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteUOM(model));
        }


    }

}
