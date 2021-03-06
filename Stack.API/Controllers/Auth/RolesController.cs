using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/Roles")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class RolesController : BaseResultHandlerController<RolesService>
    {
        public RolesController(RolesService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreateNewRole")]
        public async Task<IActionResult> CreateNewRole(RoleCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNewRole(model));
        }

        [AllowAnonymous]
        [HttpPost("UpdateRole")]
        public async Task<IActionResult> UpdateRole(EditRoleModel model)
        {
            return await EditItemResponseHandler(async () => await service.UpdateRole(model));
        }

        [AllowAnonymous]
        [HttpGet("GetSystemRoles")]
        public async Task<IActionResult> GetSystemRoles()
        {
            return await GetResponseHandler(async () => await service.GetSystemRoles());
        }

    }

}
