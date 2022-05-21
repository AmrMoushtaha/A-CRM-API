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

        //[AllowAnonymous]
        //[HttpGet("GetAllPhases")]
        //public async Task<IActionResult> GetAllPhases()
        //{
        //    return await GetResponseHandler(async () => await service.GetAllPhases());
        //}
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


    }
}
