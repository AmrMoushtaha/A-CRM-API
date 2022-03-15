using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/User")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class UsersController : BaseResultHandlerController<UsersService>
    {
        public UsersController(UsersService _service) : base(_service)
        {

        }

        [AllowAnonymous] // Allow anonymous calls without authorization to this specific endpoint . 
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            return await AddItemResponseHandler(async () => await service.LoginAsync(model));
        }


        [AllowAnonymous] // Allow anonymous calls without authorization to this specific endpoint . 
        [HttpPost("CreateDummyUser")]
        public async Task<IActionResult> CreateDummyUser(CreateDummyUserModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateDummyUser(model));
        }


    }



}
