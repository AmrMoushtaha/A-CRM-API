using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.ServiceLayer.Modules.Areas;
using Stack.ServiceLayer.Modules.CustomerStage;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/User")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ContactController : BaseResultHandlerController<ContactService>
    {
        public ContactController(ContactService _service) : base(_service)
        {

        }



    }
}
