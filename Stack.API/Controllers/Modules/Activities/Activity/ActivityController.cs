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
        [HttpPost("CreateNewActivity")]
        public async Task<IActionResult> CreateNewActivity(CreateActivityModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNewActivity(model));
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
        [HttpPost("DeleteActivity")]
        public async Task<IActionResult> DeleteActivity(DeletionModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteActivity(model));
        }

        [AllowAnonymous]
        [HttpGet("GetCurrentActivitySectionByDealID/{ActivitySectionID}")]
        public async Task<IActionResult> GetCurrentActivitySectionByDealID(long ActivitySectionID)
        {
            return await GetResponseHandler(async () => await service.GetCurrentActivitySectionByDealID(ActivitySectionID));
        }

        [AllowAnonymous]
        [HttpGet("SubmitActivity")]
        public async Task<IActionResult> SubmitActivity(ActivitySubmissionModel ID)
        {
            return await AddItemResponseHandler(async () => await service.SubmitActivity(ID));
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
