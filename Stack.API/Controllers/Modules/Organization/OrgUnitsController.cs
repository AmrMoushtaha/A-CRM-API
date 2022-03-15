using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Requests.Modules.Organizations.OrgUnits;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.Organizations;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/OrgUnits")]
    [ApiController]
    public class OrgUnitsController : BaseResultHandlerController<OrgUnitsService>
    {
        public OrgUnitsController(OrgUnitsService _service) : base(_service)
        {

        }


        [HttpPost("CreateOrgUnit")]
        public async Task<IActionResult> CreateOrgUnit(CreateOrgUnitModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateOrgUnit(model));
        }

        [HttpPost("EditOrgUnit")]
        public async Task<IActionResult> EditOrgUnit(EditOrgUnitModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditOrgUnit(model));
        }

        [HttpPost("DeleteOrgUnit")]
        public async Task<IActionResult> DeleteOrgUnit(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteOrgUnit(model));
        }

        [HttpGet("GetAllOrgUnits")]
        public async Task<IActionResult> GetAllOrgUnits()
        {
            return await GetResponseHandler(async () => await service.GetAllOrgUnits());
        }


        [HttpGet("GetAllCompanyCodeOrgUnits/{companyCodeID}")]
        public async Task<IActionResult> GetAllCompanyCodeOrgUnits(long companyCodeID)
        {
            return await GetResponseHandler(async () => await service.GetAllCompanyCodeOrgUnits(companyCodeID));
        }

        [HttpGet("GetAllAvailableOrgUnitPositions/{orgUnitID}")]
        public async Task<IActionResult> GetAllAvailableOrgUnitPositions(long orgUnitID)
        {
            return await GetResponseHandler(async () => await service.GetAllAvailableOrgUnitPositions(orgUnitID));
        }



    }

}
