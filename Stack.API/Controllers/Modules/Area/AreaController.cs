using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.ServiceLayer.Modules.Areas;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/User")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class AreaController : BaseResultHandlerController<AreaService>
    {
        public AreaController(AreaService _service) : base(_service)
        {

        }



    }
}
