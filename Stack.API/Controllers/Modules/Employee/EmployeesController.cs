using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Employees;
using System.Threading.Tasks;
using Stack.DTOs.Requests.Modules.Employees;

namespace Stack.API.Controllers.Modules.Employees
{
    [Route("api/Employees")]
    [ApiController]
    [Authorize]
    public class EmployeesController : BaseResultHandlerController<EmployeesService>
    {
        public EmployeesController(EmployeesService _service) : base(_service)
        {

        }
        //[AllowAnonymous] 
        //[HttpGet("DeleteEmployee")]
        //public async Task<IActionResult> DeleteEmployee(int ID)
        //{
        //    return await EditItemResponseHandler(async () => await service.DeleteEmployee(ID));
        //}

        [AllowAnonymous]
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            return await GetResponseHandler(async () => await service.GetAllEmployees());
        }


        [AllowAnonymous]
        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployee(long id)
        {
            return await GetResponseHandler(async () => await service.GetEmployee(id));
        }

        [AllowAnonymous]
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateEmployee(model));
        }


        [AllowAnonymous]
        [HttpPost("EditEmployeeMasterData")]
        public async Task<IActionResult> EditEmployeeMasterData(EmployeeModificationModel_MasterData model)
        {
            return await AddItemResponseHandler(async () => await service.EditEmployeeMasterData(model));
        }


        [AllowAnonymous]
        [HttpPost("EditEmployeeCommunication")]
        public async Task<IActionResult> EditEmployeeCommunication(EmployeeModificationModel_Communication model)
        {
            return await AddItemResponseHandler(async () => await service.EditEmployeeCommunication(model));
        }

        [AllowAnonymous]
        [HttpPost("AddEmployeePhoneNumber")]
        public async Task<IActionResult> AddEmployeePhoneNumber(EmployeePhoneNumberManagementModel model)
        {
            return await AddItemResponseHandler(async () => await service.AddEmployeePhoneNumber(model));
        }


        [AllowAnonymous]
        [HttpPost("RemoveEmployeePhoneNumber")]
        public async Task<IActionResult> RemoveEmployeePhoneNumber(EmployeePhoneNumberManagementModel model)
        {
            return await AddItemResponseHandler(async () => await service.RemoveEmployeePhoneNumber(model));
        }

        [AllowAnonymous]
        [HttpPost("AddEmployeeAddress")]
        public async Task<IActionResult> AddEmployeePhoneNumber(EmployeeAddressCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.AddEmployeeAddress(model));
        }


        [AllowAnonymous]
        [HttpPost("RemoveEmployeeAddress")]
        public async Task<IActionResult> RemoveEmployeeAddress(EmployeeAddressRemovalModel model)
        {
            return await AddItemResponseHandler(async () => await service.RemoveEmployeeAddress(model));
        }     
        
        [AllowAnonymous]
        [HttpPost("AddEmployeeOrgUnit")]
        public async Task<IActionResult> AddEmployeeOrgUnit(EmployeeOrgUnitCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.AddEmployeeOrgUnit(model));
        }

        //[AllowAnonymous]
        //[HttpGet("GetAllEmployeesNoQueryFilter")]
        //public async Task<IActionResult> GetAllEmployeesNoQueryFilter()
        //{
        //    return await EditItemResponseHandler(async () => await service.GetAllEmployeesNoQueryFilter());
        //}


    }



}
