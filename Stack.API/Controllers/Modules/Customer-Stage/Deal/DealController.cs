using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.CustomerStage;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/User")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class DealController : BaseResultHandlerController<DealService>
    {
        public DealController(DealService _service) : base(_service)
        {

        }

     
    }



}
