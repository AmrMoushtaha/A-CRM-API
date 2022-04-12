using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.ServiceLayer.Modules.CustomerStage;
using Stack.ServiceLayer.Modules.Zoom;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/Zoom")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ZoomController : BaseResultHandlerController<ZoomService>
    {
        public ZoomController(ZoomService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("GetSignature")]
        public async Task<IActionResult> AppendTagToContact(GetSignatureModel model)
        {
            return await AddItemResponseHandler(async () => await service.GetSignatrure(model));
        }
    }
}
