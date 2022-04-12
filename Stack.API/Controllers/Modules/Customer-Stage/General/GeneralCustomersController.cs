using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.CustomerStage;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/GeneralCustomers")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class GeneralCustomersController : BaseResultHandlerController<GeneralCustomersService>
    {

        public GeneralCustomersController(GeneralCustomersService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpGet("GetContactPossibleStages")]
        public async Task<IActionResult> GetContactPossibleStages()
        {
            return await GetResponseHandler(async () => await service.GetContactPossibleStages());
        }

        [AllowAnonymous]
        [HttpGet("GetDealPossibleStages")]
        public async Task<IActionResult> GetDealPossibleStages()
        {
            return await GetResponseHandler(async () => await service.GetDealPossibleStages());
        }


    }


}
