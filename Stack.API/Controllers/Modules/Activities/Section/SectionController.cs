using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Activities;
using Stack.ServiceLayer.Modules.Activities;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Activities
{
    [Route("api/Sections")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class SectionController : BaseResultHandlerController<SectionsService>
    {
        public SectionController(SectionsService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreateActivityTypeSection")]
        public async Task<IActionResult> CreateActivityTypeSection(CreateActivityTypeSectionModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateActivityTypeSection(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateSectionQuestion")]
        public async Task<IActionResult> CreateSectionQuestion(CreateSectionQuestionModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateSectionQuestion(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateSectionQuestionOption")]
        public async Task<IActionResult> CreateSectionQuestionOption(CreateSectionQuestionOptionModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateSectionQuestionOption(model));
        }

    }



}
