using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.pool;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Pool
{
    [Route("api/Pools")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class PoolController : BaseResultHandlerController<PoolService>
    {
        public PoolController(PoolService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpGet("GetUserAssignedPools")]
        public async Task<IActionResult> GetUserAssignedPools()
        {
            return await AddItemResponseHandler(async () => await service.GetUserAssignedPools());
        }   
        
        [AllowAnonymous]
        [HttpPost("CreatePool")]
        public async Task<IActionResult> CreatePool(PoolCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreatePool(model));
        }

        [AllowAnonymous]
        [HttpPost("AssignUsersToPool")]
        public async Task<IActionResult> AssignUsersToPool(PoolAssignmentModel model)
        {
            return await AddItemResponseHandler(async () => await service.AssignUsersToPool(model));
        }

    }



}
