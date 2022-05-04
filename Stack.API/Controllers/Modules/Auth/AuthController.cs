using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/Auth")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class AuthController : BaseResultHandlerController<AuthService>
    {
        public AuthController(AuthService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreateAdministratorAccount")]
        public async Task<IActionResult> CreateAdministratorAccount(UserCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateAdministratorAccount(model));
        }

        [AllowAnonymous]
        [HttpPost("UpdateRoleAuthorizations")]
        public async Task<IActionResult> UpdateRoleAuthorizations(AuthorizationsModel model)
        {
            return await EditItemResponseHandler(async () => await service.UpdateRoleAuthorizations(model));
        }


        [AllowAnonymous]
        [HttpPost("UpdateUserAuthorizations")]
        public async Task<IActionResult> UpdateUserAuthorizations(UpdateUserAuthModel model)
        {
            return await EditItemResponseHandler(async () => await service.UpdateUserAuthorizations(model));
        }

        [AllowAnonymous]
        [HttpGet("GetAuthModelByRoleID")]
        public async Task<IActionResult> GetAuthModelByRoleID(string RoleID)
        {
            return await GetResponseHandler(async () => await service.GetAuthModelByRoleID(RoleID));
        }

        [AllowAnonymous]
        [HttpGet("GetAuthModelByUserID")]
        public async Task<IActionResult> GetAuthModelByUserID(string UserID)
        {
            return await GetResponseHandler(async () => await service.GetAuthModelByUserID(UserID));
        }

        [AllowAnonymous]
        [HttpGet("GetSystemAuthModel")]
        public async Task<IActionResult> GetSystemAuthModel()
        {
            return await GetResponseHandler(async () => await service.GetSystemAuthModel());
        }

    }

}
