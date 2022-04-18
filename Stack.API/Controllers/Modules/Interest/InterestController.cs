using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Interest;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class InterestController : BaseResultHandlerController<InterestService>
    {
        public InterestController(InterestService _service) : base(_service)
        {

        }

        #region  Interest 
       
        [HttpGet("{level}")]
        public async Task<IActionResult> GetInterestByLevel(int level)
        {
            return await AddItemResponseHandler(async () => await service.Get_InterestByLevel(level));
        }

       
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetInterestByParentID(int ID)
        {
            return await AddItemResponseHandler(async () => await service.Get_InterestByParentID(ID));
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateInterest(LInterestToAdd interestToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Interest(interestToAdd));
        }

       
        [HttpGet("{ID}")]
        public async Task<IActionResult> DeleteInterest(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Interest(ID));
        }
        #endregion


        //#region Interest Input
        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> CreateLInterestInput(LInterestInputToAdd LInterestInputToAdd)
        //{
        //    return await AddItemResponseHandler(async () => await service.Create_LInterestInput(LInterestInputToAdd));
        //}

        //[AllowAnonymous]
        //[HttpGet("{ID}")]
        //public async Task<IActionResult> DeleteLInterestInput(long ID)
        //{
        //    return await GetResponseHandler(async () => await service.Delete_LInterestInput(ID));
        //}

        //[AllowAnonymous]
        //[HttpGet("{ID}")]
        //public async Task<IActionResult> GetLInterestByInputID(long ID)
        //{
        //    return await GetResponseHandler(async () => await service.Get_LInterestByInputID(ID));
        //}
        //#endregion


    }
}
