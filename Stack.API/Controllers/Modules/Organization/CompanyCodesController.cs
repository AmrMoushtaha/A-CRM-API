using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Requests.Modules.Organizations.CompanyCodes;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Organizations;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/CompanyCodes")]
    [ApiController]
    [Authorize]
    public class CompanyCodesController : BaseResultHandlerController<CompanyCodesService>
    {
        public CompanyCodesController(CompanyCodesService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreateCompanyCode")]
        public async Task<IActionResult> CreateCompanyCode(CreateCompanyCodeModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateCompanyCode(model));
        }

        [AllowAnonymous]
        [HttpPost("EditCompanyCode")]
        public async Task<IActionResult> EditCompanyCode(EditCompanyCodeModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditCompanyCode(model));
        }

        [AllowAnonymous]
        [HttpPost("DeleteCompanyCode")]
        public async Task<IActionResult> DeleteCompanyCode(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteCompanyCode(model));
        }


        [AllowAnonymous]
        [HttpGet("GetAllCompanyCodes")]
        public async Task<IActionResult> GetAllCompanyCodes()
        {
            return await GetResponseHandler(async () => await service.GetAllCompanyCodes());
        }

    }

}
