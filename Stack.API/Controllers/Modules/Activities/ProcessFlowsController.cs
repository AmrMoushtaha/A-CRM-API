using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.ServiceLayer.Modules.Activities;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Activities
{
    [Route("api/ProcessFlows")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ProcessFlowsController : BaseResultHandlerController<ProcessFlowService>
    {
        public ProcessFlowsController(ProcessFlowService _service) : base(_service)
        {

        }


        [AllowAnonymous]
        [HttpPost("CreateProcessFlow")]
        public async Task<IActionResult> CreateProcessFlow(CreateProcessFlowModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateProcessFlow(model));
        }


    }



}
