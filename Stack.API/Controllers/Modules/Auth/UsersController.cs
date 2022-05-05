using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models.Modules.Auth;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseResultHandlerController<UsersService>
    {
        public UsersController(UsersService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            return await AddItemResponseHandler(async () => await service.LoginAsync(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser(UserCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNewUser(model));
        }

        [AllowAnonymous]
        [HttpPost("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserModel model)
        {
            return await EditItemResponseHandler(async () => await service.UpdateUserDetails(model));
        }

        [AllowAnonymous]
        [HttpPost("UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword(UpdatePasswordModel model)
        {
            return await EditItemResponseHandler(async () => await service.UpdateUserPassword(model));
        }

        [AllowAnonymous]
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            return await GetResponseHandler(async () => await service.GetUserDetails());
        }


        [AllowAnonymous]
        [HttpGet("GetAllSystemUsers")]
        public async Task<IActionResult> GetAllSystemUsers()
        {
            return await GetResponseHandler(async () => await service.GetAllSystemUsers());
        }

    }



}
