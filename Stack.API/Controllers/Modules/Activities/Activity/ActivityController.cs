using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models.Modules.Activities;
using Stack.DTOs.Models.Shared;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.ServiceLayer.Modules.Activities;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Activities
{
    [Route("api/Activities")]
    [ApiController]
    [Authorize] //Require Authorization to access API endpoints . 
    public class ActivityController : BaseResultHandlerController<ActivitiesService>
    {
        public ActivityController(ActivitiesService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreateActivityType")]
        public async Task<IActionResult> CreateActivityType(CreateActivityTypeModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateActivityType(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateNewContactActivity")]
        public async Task<IActionResult> CreateNewActivity(CreateContactActivityModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNewContactActivity(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateNewDealActivity")]
        public async Task<IActionResult> CreateNewActivity(CreateDealActivityModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNewDealActivity(model));
        }

        [AllowAnonymous]
        [HttpGet("GetAllActiveActivityTypes")]
        public async Task<IActionResult> GetAllActiveActivityTypes()
        {
            return await AddItemResponseHandler(async () => await service.GetAllActiveActivityTypes());
        }

        [AllowAnonymous]
        [HttpPost("GetNextActivitySection")]
        public async Task<IActionResult> GetNextActivitySection(SectionToAnswer model)
        {
            return await GetResponseHandler(async () => await service.GetNextActivitySection(model));
        }


        [AllowAnonymous]
        [HttpPost("GetPreviousActivitySection")]
        public async Task<IActionResult> GetPreviousActivitySection(SectionToAnswer model)
        {
            return await GetResponseHandler(async () => await service.GetPreviousActivitySection(model));
        }

        [AllowAnonymous]
        [HttpPost("DeleteActivity")]
        public async Task<IActionResult> DeleteActivity(DeletionModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteActivity(model));
        }

        [AllowAnonymous]
        [HttpGet("GetCurrentActivitySectionByDealID/{ID}")]
        public async Task<IActionResult> GetCurrentActivitySectionByDealID(long ID)
        {
            return await GetResponseHandler(async () => await service.GetCurrentActivitySectionByDealID(ID));
        }

        [AllowAnonymous]
        [HttpGet("GetCurrentActivitySectionByContactID/{ID}")]
        public async Task<IActionResult> GetCurrentActivitySectionByContactID(long ID)
        {
            return await GetResponseHandler(async () => await service.GetCurrentActivitySectionByContactID(ID));
        }

        [AllowAnonymous]
        [HttpPost("SubmitActivity")]
        public async Task<IActionResult> SubmitActivity(ActivitySubmissionModel model)
        {
            return await AddItemResponseHandler(async () => await service.SubmitActivity(model));
        }

        [AllowAnonymous]
        [HttpGet("GetActivityHistoryByContactID/{ContactID}")]
        public async Task<IActionResult> GetActivityHistoryByContactID(long ContactID)
        {
            return await AddItemResponseHandler(async () => await service.GetActivityHistoryByContactID(ContactID));
        }

        [AllowAnonymous]
        [HttpGet("GetActivityHistoryByDealID/{DealID}")]
        public async Task<IActionResult> GetActivityHistoryByDealID(long DealID)
        {
            return await AddItemResponseHandler(async () => await service.GetActivityHistoryByDealID(DealID));
        }

    }

}
