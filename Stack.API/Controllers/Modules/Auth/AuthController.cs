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
        [HttpGet("InitializeSystem/{Password}")]
        public async Task<IActionResult> InitializeSystem(string Password)
        {
            return await GetResponseHandler(async () => await service.InitializeSystem(Password));
        }


        [AllowAnonymous]
        [HttpPost("CreateAdministrator")]
        public async Task<IActionResult> CreateAdministrator(CreateDummyUserModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateAdministrator(model));
        }

    }



}
