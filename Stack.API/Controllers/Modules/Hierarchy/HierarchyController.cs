using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Hierarchy;
using Stack.DTOs.Requests.Modules.Interest;
using Stack.ServiceLayer.Modules.Hierarchy;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize] // Require Authorization to access API endpoints . 
    public class HierarchyController : BaseResultHandlerController<HierarchyService>
    {
        public HierarchyController(HierarchyService _service) : base(_service)
        {

        }

        #region Level

        [HttpGet]
        public async Task<IActionResult> GetLevels()
        {
            return await GetResponseHandler(async () => await service.Get_Levels());
        }

        [HttpPost]
        public async Task<IActionResult> CreateLevel(LevelToAdd LevelToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Level(LevelToAdd));
        }

        [HttpPost]
        public async Task<IActionResult> EditLevel(LevelToEdit LevelToEdit)
        {
            return await AddItemResponseHandler(async () => await service.Edit_Level(LevelToEdit));
        }
        #endregion


        #region Section

        [HttpGet]
        public async Task<IActionResult> GetSections()
        {
            return await GetResponseHandler(async () => await service.Get_Sections());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSection(SectionToAdd SectionToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Section(SectionToAdd));
        }

        [HttpPost]
        public async Task<IActionResult> EditSection(SectionToEdit SectionToEdit)
        {
            return await AddItemResponseHandler(async () => await service.Edit_Section(SectionToEdit));
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetSectionsByLevelID(long ID)
        {
            return await GetResponseHandler(async () => await service.Get_SectionsByLevelID(ID));
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> DeleteSectionByID(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_SectionByID(ID));
        }
        #endregion



        #region Input

        [HttpGet]
        public async Task<IActionResult> GetInputs()
        {
            return await GetResponseHandler(async () => await service.Get_Inputs());
        }

        [HttpPost]
        public async Task<IActionResult> CreateInput(InputToAdd InputToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Input(InputToAdd));
        }

        [HttpPost]
        public async Task<IActionResult> EditInput(InputToEdit InputToEdit)
        {
            return await AddItemResponseHandler(async () => await service.Edit_Input(InputToEdit));
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetInputsByLevelID(long ID)
        {
            return await GetResponseHandler(async () => await service.Get_InputsByLevelID(ID));
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetInputsBySectionID(long ID)
        {
            return await GetResponseHandler(async () => await service.Get_InputsBySectionID(ID));
        }
        [HttpGet("{ID}")]
        public async Task<IActionResult> DeleteInput(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Input(ID));
        }
        #endregion


        #region Attribute

        [HttpGet]
        public async Task<IActionResult> GetAttributes()
        {
            return await GetResponseHandler(async () => await service.Get_Attributes());
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPredefinedAttributes()
        {
            return await GetResponseHandler(async () => await service.GetPredefinedAttributes());
        }


        [HttpPost]
        public async Task<IActionResult> CreateAttribute(AttributeToAdd AttributeToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Create_Attribute(AttributeToAdd));
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> DeleteAttribute(long ID)
        {
            return await GetResponseHandler(async () => await service.Delete_Attribute(ID));
        }

        [HttpPost]
        public async Task<IActionResult> EditAttribute(AttributeToEdit AttributeToAdd)
        {
            return await AddItemResponseHandler(async () => await service.Edit_Attribute(AttributeToAdd));
        }

        #endregion


    }
}
