using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.CustomerStage;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/GeneralCustomers")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class GeneralCustomersController : BaseResultHandlerController<GeneralCustomersService>
    {

        public GeneralCustomersController(GeneralCustomersService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpGet("GetContactPossibleStages")]
        public async Task<IActionResult> GetContactPossibleStages()
        {
            return await GetResponseHandler(async () => await service.GetContactPossibleStages());
        }

        [AllowAnonymous]
        [HttpPost("GetCurrentStageRecord")]
        public async Task<IActionResult> GetCurrentStageRecord(GetPoolRecordsModel model)
        {
            return await AddItemResponseHandler(async () => await service.GetCurrentStageRecord(model));
        }

        [AllowAnonymous]
        [HttpGet("GetDealPossibleStages")]
        public async Task<IActionResult> GetDealPossibleStages()
        {
            return await GetResponseHandler(async () => await service.GetDealPossibleStages());
        }


        [AllowAnonymous]
        [HttpGet("GetAllJunkedRecords/{customerStage}")]
        public async Task<IActionResult> GetAllJunkedRecords(int customerStage)
        {
            return await GetResponseHandler(async () => await service.GetAllJunkedRecords(customerStage));
        }


        [AllowAnonymous]
        [HttpGet("GetAllNotInterestedRecords/{customerStage}")]
        public async Task<IActionResult> GetAllNotInterestedRecords(int customerStage)
        {
            return await GetResponseHandler(async () => await service.GetAllNotInterestedRecords(customerStage));
        }

        #region Creation

        [AllowAnonymous]
        [HttpPost("CreateNewDeal")]
        public async Task<IActionResult> CreateNewDeal(NewDealCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNewDeal(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateSingleStageRecord_AssignToUser")]
        public async Task<IActionResult> CreateSingleStageRecord_AssignToUser(RecordCreationModel model)
        {
            return await GetResponseHandler(async () => await service.CreateSingleStageRecord_AssignToUser(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateSingleStageRecord_AssignToSelf")]
        public async Task<IActionResult> CreateSingleStageRecord_AssignToSelf(RecordCreationModel model)
        {
            return await GetResponseHandler(async () => await service.CreateSingleStageRecord_AssignToSelf(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateSingleStageRecord_Unassigned")]
        public async Task<IActionResult> CreateSingleStageRecord_Unassigned(RecordCreationModel model)
        {
            return await GetResponseHandler(async () => await service.CreateSingleStageRecord_Unassigned(model));
        }
        #endregion


        #region Favorites
        [AllowAnonymous]
        [HttpPost("GetUserFavorites")]
        public async Task<IActionResult> GetUserFavorites(GetFavoritesModel model)
        {
            return await AddItemResponseHandler(async () => await service.GetUserFavorites(model));
        }

        [AllowAnonymous]
        [HttpPost("SetRecordFavorite")]
        public async Task<IActionResult> SetRecordFavorite(SetRecordFavoriteModel model)
        {
            return await AddItemResponseHandler(async () => await service.SetRecordFavorite(model));
        }
        #endregion

    }


}
