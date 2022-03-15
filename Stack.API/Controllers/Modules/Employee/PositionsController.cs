using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Employees;
using System.Threading.Tasks;
using Stack.DTOs.Requests.Modules.Employees;
using Stack.DTOs.Requests.Shared;

namespace Stack.API.Controllers.Modules.Employees
{
    [Route("api/Positions")]
    [ApiController]

    public class PositionsController : BaseResultHandlerController<PositionsService>
    {
        public PositionsController(PositionsService _service) : base(_service)
        {

        }

        [HttpGet("GetAllPositions")]
        public async Task<IActionResult> GetAllPositions()
        {
            return await GetResponseHandler(async () => await service.GetAllPositions());
        }

        [HttpGet("GetAllPositionsByOrgUnitID/{id}")]
        public async Task<IActionResult> GetPositionsByOrgUnitID(int id)
        {
            return await GetResponseHandler(async () => await service.GetPositionsByOrgUnitID(id));
        }


        [HttpPost("CreatePosition")]
        public async Task<IActionResult> CreatePosition(PositionCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreatePosition(model));
        }


        [HttpPost("EditPosition")]
        public async Task<IActionResult> EditPosition(EditPositionModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditPosition(model));
        }

        [HttpPost("DeletePosition")]
        public async Task<IActionResult> DeletePosition(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeletePosition(model));
        }

    }

}
