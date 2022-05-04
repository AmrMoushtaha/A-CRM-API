using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.AreaInterest;
using Stack.ServiceLayer.Modules.Interest;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Location
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class LocationController : BaseResultHandlerController<LocationService>
    {
        public LocationController(LocationService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationToAdd LocationToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Location(LocationToAdd));
        }

        [AllowAnonymous]
        [HttpGet("{type}")]
        public async Task<IActionResult> GetLocationByType(int type)
        {
            return await AddItemResponseHandler(async () => await service.Get_LocationByType(type));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            return await AddItemResponseHandler(async () => await service.Get_Locations());
        }

        [AllowAnonymous]
        [HttpGet("{ParentID}")]
        public async Task<IActionResult> GetLocationByParentID(long ParentID)
        {
            return await AddItemResponseHandler(async () => await service.Get_LocationByParentID(ParentID));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> EditLocation(LocationToEdit LocationToEdit)
        {
            return await AddItemResponseHandler(async () => await service.Edit_Location(LocationToEdit));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> DeleteLocation(long ID)
        {
            return await AddItemResponseHandler(async () => await service.Delete_Location(ID));
        }


    }
}
