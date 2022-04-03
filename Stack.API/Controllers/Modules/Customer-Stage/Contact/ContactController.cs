using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.CustomerStage;
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

        [AllowAnonymous]
        [HttpPost("CreateContact")]
        public async Task<IActionResult> CreateContact(ContactCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateContact(model));
        }

        [AllowAnonymous]
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(AddCommentModel model)
        {
            return await AddItemResponseHandler(async () => await service.AddComment(model));
        }

        [AllowAnonymous]
        [HttpGet("GetAvailableTags/{id}")]
        public async Task<IActionResult> GetAvailableTags(long id)
        {
            return await GetResponseHandler(async () => await service.GetAvailableTags(id));
        }

        [AllowAnonymous]
        [HttpPost("AppendTagToContact")]
        public async Task<IActionResult> AppendTagToContact(TagAppendanceModel model)
        {
            return await AddItemResponseHandler(async () => await service.AppendTagToContact(model));
        }

        [AllowAnonymous]
        [HttpGet("GetAvailableContactStatuses")]
        public async Task<IActionResult> GetAvailableContactStatuses()
        {
            return await GetResponseHandler(async () => await service.GetAvailableContactStatuses());
        }

        [AllowAnonymous]
        [HttpGet("GetAssignedContacts")]
        public async Task<IActionResult> GetAssignedContacts()
        {
            return await GetResponseHandler(async () => await service.GetAssignedContacts());
        }
    }
}
