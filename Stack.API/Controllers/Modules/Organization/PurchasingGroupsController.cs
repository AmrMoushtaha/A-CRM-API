using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Organizations.PurchasingGroups;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Organizations;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/PurchasingGroups")]
    [ApiController]

    public class PurchasingGroupsController : BaseResultHandlerController<PurchasingGroupsService>
    {
        public PurchasingGroupsController(PurchasingGroupsService _service) : base(_service)
        {

        }

        [HttpPost("CreatePurchasingGroup")]
        public async Task<IActionResult> CreatePurchasingGroup(CreatePGModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreatePurchasingGroup(model));
        }

        [HttpPost("UpdatePurchasingGroup")]
        public async Task<IActionResult> UpdatePurchasingGroup(EditPGModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditPurchasingGroup(model));
        }

        [HttpPost("DeletePurchasingGroup")]
        public async Task<IActionResult> DeletePurchasingGroup(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeletePurchasingGroup(model));
        }

        [HttpGet("GetAllPurchasingGroups")]
        public async Task<IActionResult> GetAllPurchasingGroups()
        {
            return await GetResponseHandler(async () => await service.GetAllPurchasingGroups());
        }


    }

}
