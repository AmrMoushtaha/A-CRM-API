using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Region;
using Stack.ServiceLayer.Modules.SystemInitialization;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.SystemInitialization
{
    [Route("api/SystemInitialization")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class SystemInitializationController : BaseResultHandlerController<SystemInitializationService>
    {
        public SystemInitializationController(SystemInitializationService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("InitializeDefaultActivityTypes")]
        public async Task<IActionResult> GetNextActivitySection()
        {
            return await AddItemResponseHandler(async () => await service.InitializeDefaultSystemActivityTypes());
        }

        [AllowAnonymous]
        [HttpPost("InitializeSystemAuthorizationsScheme")]
        public async Task<IActionResult> InitializeSystemAuthorizationsScheme()
        {
            return await AddItemResponseHandler(async () => await service.InitializeSystemAuthorizationsScheme());
        }


        [AllowAnonymous]
        [HttpGet("InitializeSystem/{Password}")]
        public async Task<IActionResult> InitializeSystem(string Password)
        {
            return await GetResponseHandler(async () => await service.InitializeSystem(Password));
        }




    }



}
