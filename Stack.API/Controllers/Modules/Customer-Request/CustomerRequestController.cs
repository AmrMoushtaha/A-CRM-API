using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models.Modules.CR;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.ServiceLayer.Modules.CR;
using Stack.ServiceLayer.Modules.CustomerStage;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.CR
{
    [Route("api/CustomerRequest")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class CustomerRequestController : BaseResultHandlerController<CustomerRequestService>
    {
        public CustomerRequestController(CustomerRequestService _service) : base(_service)
        {

        }

        #region Phase

        [AllowAnonymous]
        [HttpGet("GetAllPhases")]
        public async Task<IActionResult> GetAllPhases()
        {
            return await GetResponseHandler(async () => await service.GetAllPhases());
        }
        //[AllowAnonymous]
        //[HttpGet("GetPhaseByID/{id}")]
        //public async Task<IActionResult> GetPhaseByID(long id)
        //{
        //    return await GetResponseHandler(async () => await service.GetPhaseByID(id));
        //}

        [AllowAnonymous]
        [HttpPost("CreatePhase")]
        public async Task<IActionResult> CreatePhase(PhaseCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreatePhase(model));
        }


        #endregion


        #region Timeline

        [AllowAnonymous]
        [HttpGet("GetAllTimelines")]
        public async Task<IActionResult> GetAllTimelines()
        {
            return await GetResponseHandler(async () => await service.GetAllTimelines());
        }

        [AllowAnonymous]
        [HttpGet("GetTimelineByID/{timelineID}")]
        public async Task<IActionResult> GetTimelineByID(long timelineID)
        {
            return await GetResponseHandler(async () => await service.GetTimelineByID(timelineID));
        }

        [AllowAnonymous]
        [HttpPost("CreateTimeline")]
        public async Task<IActionResult> CreateTimeline(CRTimelineCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateTimeline(model));
        }

        [AllowAnonymous]
        [HttpPost("LinkPhasesToTimeline")]
        public async Task<IActionResult> LinkPhasesToTimeline(CRTimelineCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.LinkPhasesToTimeline(model));
        }

        #endregion


        #region Customer Request

        [AllowAnonymous]
        [HttpPost("CreateCustomerRequestType")]
        public async Task<IActionResult> CreateCustomerRequestType(CRTypeCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateCustomerRequestType(model));
        }

        [AllowAnonymous]
        [HttpGet("GetAllCustomerRequestTypes")]
        public async Task<IActionResult> GetAllCustomerRequestTypes()
        {
            return await GetResponseHandler(async () => await service.GetAllRequestTypes());
        }

        [AllowAnonymous]
        [HttpGet("GetCustomerRequestTypeByID/{id}")]
        public async Task<IActionResult> GetCustomerRequestTypeByID(long id)
        {
            return await GetResponseHandler(async () => await service.GetRequestTypeByID(id));
        }


        #region Agent Customer Request Interactions

        [AllowAnonymous]
        [HttpGet("GetCustomerRequestQuickViewByContactID/{id}")]
        public async Task<IActionResult> GetCustomerRequestQuickViewByContactID(long id)
        {
            return await GetResponseHandler(async () => await service.GetCustomerRequestQuickViewByContactID(id));
        }

        [AllowAnonymous]
        [HttpGet("GetCustomerRequestQuickViewByDealID/{id}")]
        public async Task<IActionResult> GetCustomerRequestQuickViewByDealID(long id)
        {
            return await GetResponseHandler(async () => await service.GetCustomerRequestQuickViewByDealID(id));
        }

        [AllowAnonymous]
        [HttpGet("GetCustomerRequestsByContactID/{id}")]
        public async Task<IActionResult> GetCustomerRequestsByContactID(long id)
        {
            return await GetResponseHandler(async () => await service.GetCustomerRequestsByContactID(id));
        }

        [AllowAnonymous]
        [HttpGet("GetCustomerRequestsByDealID/{id}")]
        public async Task<IActionResult> GetCustomerRequestsByDealID(long id)
        {
            return await GetResponseHandler(async () => await service.GetCustomerRequestsByDealID(id));
        }

        [AllowAnonymous]
        [HttpGet("GetCustomerRequestByID/{id}")]
        public async Task<IActionResult> GetCustomerRequestByID(long id)
        {
            return await GetResponseHandler(async () => await service.GetCustomerRequestByID(id));
        }

        [AllowAnonymous]
        [HttpGet("GetActiveRequestTypes")]
        public async Task<IActionResult> GetActiveRequestTypes()
        {
            return await GetResponseHandler(async () => await service.GetActiveRequestTypes());
        }

        [AllowAnonymous]
        [HttpPost("CreateCustomerRequest")]
        public async Task<IActionResult> CreateCustomerRequest(CustomerRequestCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateCustomerRequest(model));
        }

        [AllowAnonymous]
        [HttpPost("UpdateCustomerRequestPhase")]
        public async Task<IActionResult> UpdateCustomerRequestPhase(CRUpdatePhaseModel model)
        {
            return await AddItemResponseHandler(async () => await service.UpdateCustomerRequestPhase(model));
        }
        #endregion
        #endregion

    }
}
