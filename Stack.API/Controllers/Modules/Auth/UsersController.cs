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


        //[AllowAnonymous] // Allow anonymous calls without authorization to this specific endpoint . 
        //[HttpGet("SeedDB")]
        //public async Task<IActionResult> SeedDB()
        //{
        //    return await GetResponseHandler(async () => await service.SeedDB());
        //}



        [AllowAnonymous]
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            return await GetResponseHandler(async () => await service.GetUserDetails());
        }
    }



}
