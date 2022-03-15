using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Region;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Region
{
    [Route("api/User")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class RegionController : BaseResultHandlerController<RegionService>
    {
        public RegionController(RegionService _service) : base(_service)
        {

        }


    }



}
