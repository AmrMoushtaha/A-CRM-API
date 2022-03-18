using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.ServiceLayer.Modules.Areas;
using Stack.ServiceLayer.Modules.CustomerStage;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/Contacts")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ContactController : BaseResultHandlerController<ContactService>
    {
        public ContactController(ContactService _service) : base(_service)
        {

        }


        [AllowAnonymous]
        [HttpGet("GetContactDetails/{id}")]
        public async Task<IActionResult> GetContactDetails(long id)
        {
            return await GetResponseHandler(async () => await service.GetContact(id));
        }

    }
}
