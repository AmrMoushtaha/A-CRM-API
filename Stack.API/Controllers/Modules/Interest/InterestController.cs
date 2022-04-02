using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Interest;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class InterestController : BaseResultHandlerController<InterestService>
    {
        public InterestController(InterestService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create_Interest(LInterestToAdd interestToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Interest(interestToAdd));
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Delete_Interest(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Interest(ID));
        }

    }
}
