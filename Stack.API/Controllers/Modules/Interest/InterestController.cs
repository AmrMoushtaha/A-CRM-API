using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.DTOs.Requests.Modules.Auth;
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
        [AllowAnonymous]
        [HttpGet("{level}")]
        public async Task<IActionResult> Get_InterestByLevel(int level)
        {
            return await AddItemResponseHandler(async () => await service.Get_InterestByLevel(level));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get_InterestByParentID(int ID)
        {
            return await AddItemResponseHandler(async () => await service.Get_InterestByParentID(ID));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create_Interest(LInterestToAdd interestToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Interest(interestToAdd));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> Delete_Interest(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Interest(ID));
        }
        #endregion

        #region Input

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get_Inputs( )
        {
            return await GetResponseHandler(async () => await service.Get_Inputs());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Edit_Input(InputToEdit InputToEdit)
        {
            return await AddItemResponseHandler(async () => await service.Edit_Input(InputToEdit));
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create_Input(InputToAdd InputToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Input(InputToAdd));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> Delete_Input(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Input(ID));
        }
        #endregion

        #region Interest Input
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create_LInterestInput(LInterestInputToAdd LInterestInputToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_LInterestInput(LInterestInputToAdd));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Delete_LInterestInput(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_LInterestInput(ID));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get_LInterestByInputID(long ID)
        {
            return await GetResponseHandler(async () => await service.Get_LInterestByInputID(ID));
        }
        #endregion

        #region Attribute

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get_Attributes( )
        {
            return await GetResponseHandler(async () => await service.Get_Attributes());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create_Attribute(AttributeToAdd AttributeToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Attribute(AttributeToAdd));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> Delete_Attribute(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Attribute(ID));
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Edit_Attribute(AttributeToEdit AttributeToEdit)
        {
            return await AddItemResponseHandler(async () => await service.Edit_Attribute(AttributeToEdit));
        }
        #endregion

        #region Attribute

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get_LInterestByAttributeID(long ID)
        {
            return await GetResponseHandler(async () => await service.Get_LInterestByAttributeID(ID));
        }

        [AllowAnonymous]
        [HttpGet("{ID}")]
        public async Task<IActionResult> Delete_LInterestAttribute(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_LInterestAttribute(ID));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create_LInterestAttribute(InterestAttributeToAdd InterestAttributeToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_LInterestAttribute(InterestAttributeToAdd));
        }
        #endregion
    }
}
