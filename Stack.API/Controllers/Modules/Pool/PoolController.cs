using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models.Modules.Pool;
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
            return await GetResponseHandler(async () => await service.GetUserAssignedPools());
        }

        [AllowAnonymous]
        [HttpGet("GetUserAssignedPoolsByUserID/{userID}")]
        public async Task<IActionResult> GetUserAssignedPoolsByUserID(string userID)
        {
            return await GetResponseHandler(async () => await service.GetUserAssignedPoolsByUserID(userID));
        }

        [AllowAnonymous]
        [HttpGet("GetPoolAssignedUsers/{poolID}")]
        public async Task<IActionResult> GetPoolAssignedUsers(long poolID)
        {
            return await GetResponseHandler(async () => await service.GetPoolAssignedUsers(poolID));
        }

        [AllowAnonymous]
        [HttpGet("GetPoolAssignedUsers_ExcludeAdmins/{poolID}")]
        public async Task<IActionResult> GetPoolAssignedUsers_ExcludeAdmins(long poolID)
        {
            return await GetResponseHandler(async () => await service.GetPoolAssignedUsers_ExcludeAdmins(poolID));
        }


        [AllowAnonymous]
        [HttpGet("GetPoolAssignedAdmins/{poolID}")]
        public async Task<IActionResult> GetPoolAssignedAdmins(long poolID)
        {
            return await GetResponseHandler(async () => await service.GetPoolAssignedAdmins(poolID));
        }


        [AllowAnonymous]
        [HttpGet("GetPoolConfiguration/{poolID}")]
        public async Task<IActionResult> GetPoolConfiguration(long poolID)
        {
            return await GetResponseHandler(async () => await service.GetPoolConfiguration(poolID));
        }

        [AllowAnonymous]
        [HttpGet("GetPoolDetails/{poolID}")]
        public async Task<IActionResult> GetPoolDetails(long poolID)
        {
            return await GetResponseHandler(async () => await service.GetPoolDetails(poolID));
        }

        [AllowAnonymous]
        [HttpGet("GetPoolContacts/{poolID}")]
        public async Task<IActionResult> GetPoolContacts(long poolID)
        {
            return await GetResponseHandler(async () => await service.GetPoolContacts(poolID));
        }

        [AllowAnonymous]
        [HttpPost("GetPoolAssignedUsersCapacity")]
        public async Task<IActionResult> GetPoolAssignedUsersCapacity(GetPoolAssignedUsersCapacityModel model)
        {
            return await AddItemResponseHandler(async () => await service.GetPoolAssignedUsersCapacity(model));
        }

        [AllowAnonymous]
        [HttpPost("UpdateUsersCapacity")]
        public async Task<IActionResult> UpdateUsersCapacity(UpatePoolUsersCapacityModel model)
        {
            return await AddItemResponseHandler(async () => await service.UpdateUsersCapacity(model));
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

        [AllowAnonymous]
        [HttpPost("ViewRecord_VerifyUser")]
        public async Task<IActionResult> ViewRecord_VerifyUser(VerifyRecordModel model)
        {
            return await AddItemResponseHandler(async () => await service.ViewRecord_VerifyUser(model));
        }

        [AllowAnonymous]
        [HttpGet("LogUsersActivePool/{poolID}")]
        public async Task<IActionResult> LogUsersActivePool(long poolID)
        {
            return await GetResponseHandler(async () => await service.LogUsersActivePool(poolID));
        }

        [AllowAnonymous]
        [HttpPost("LockRecord")]
        public async Task<IActionResult> LockRecord(LockRecordModel model)
        {
            return await GetResponseHandler(async () => await service.LockRecord(model));
        }

    }



}
