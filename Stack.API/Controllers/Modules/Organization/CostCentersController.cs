using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Requests.Modules.Organizations.CostCenters;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Organizations;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/CostCenters")]
    [ApiController]
    
    public class CostCentersController : BaseResultHandlerController<CostCentersService>
    {
        public CostCentersController(CostCentersService _service) : base(_service)
        {

        }

        [HttpPost("CreateCostCenter")]
        public async Task<IActionResult> CreateCostCenter(CreateCostCenterModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateCostCenter(model));
        }


        [HttpPost("EditCostCenter")]
        public async Task<IActionResult> EditCostCenter(EditCostCenterModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditCostCenter(model));
        }

        [HttpPost("DeleteCostCenter")]
        public async Task<IActionResult> DeleteCostCenter(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteCostCenter(model));
        }

        [HttpGet("GetAllCostCenters")]
        public async Task<IActionResult> GetAllCostCenters()
        {
            return await GetResponseHandler(async () => await service.GetAllCostCenters());
        }

        [HttpGet("GetCostCentersByCompanyCodeID/{ID}")]
        public async Task<IActionResult> GetCostCentersByCompanyCodeID(int ID)
        {
            return await GetResponseHandler(async () => await service.GetCostCentersByCompanyCodeID(ID));
        }

    }

}
