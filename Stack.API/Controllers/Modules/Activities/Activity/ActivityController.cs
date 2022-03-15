using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.ServiceLayer.Modules.Activities;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Activities
{
    [Route("api/Activities")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ActivityController : BaseResultHandlerController<ActivitiesService>
    {
        public ActivityController(ActivitiesService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreaCreateActivityType")]
        public async Task<IActionResult> CreateActivityType(CreateActivityTypeModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateActivityType(model));
        }



    }



}
