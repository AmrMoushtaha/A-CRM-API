using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.CustomerStage;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.ServiceLayer.Modules.Channels;
using Stack.ServiceLayer.Modules.CustomerStage;
using Stack.ServiceLayer.Modules.Zoom;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/Channels")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ChannelController : BaseResultHandlerController<ChannelsService>
    {
        public ChannelController(ChannelsService _service) : base(_service) { }

        //Get
        [AllowAnonymous]
        [HttpGet("GetAllChannels")]
        public async Task<IActionResult> GetAllChannels()
        {
            return await GetResponseHandler(async () => await service.GetAllChannels());
        }
        [AllowAnonymous]
        [HttpGet("GetAllLeadSourceTypes")]
        public async Task<IActionResult> GetAllLeadSourceTypes()
        {
            return await GetResponseHandler(async () => await service.GetAllLeadSourceTypes());
        }
        [AllowAnonymous]
        [HttpGet("GetAllLeadSourceNames")]
        public async Task<IActionResult> GetAllLeadSourceNames()
        {
            return await GetResponseHandler(async () => await service.GetAllLeadSourceNames());
        }
        [AllowAnonymous]
        [HttpGet("GetChannelByID/{id}")]
        public async Task<IActionResult> GetChannelByID(long id)
        {
            return await GetResponseHandler(async () => await service.GetChannelByID(id));
        }
        [AllowAnonymous]
        [HttpGet("GetLeadSourceTypeByID/{id}")]
        public async Task<IActionResult> GetLeadSourceTypeByID(long id)
        {
            return await GetResponseHandler(async () => await service.GetLeadSourceTypeByID(id));
        }
        [AllowAnonymous]
        [HttpGet("GetLeadSourceNameByID/{id}")]
        public async Task<IActionResult> GetLeadSourceNameByID(long id)
        {
            return await GetResponseHandler(async () => await service.GetLeadSourceNameByID(id));
        }
        [AllowAnonymous]
        [HttpGet("GetLeadSourceTypesByChannelID/{id}")]
        public async Task<IActionResult> GetLeadSourceTypesByChannelID(long id)
        {
            return await GetResponseHandler(async () => await service.GetLeadSourceTypesByChannelID(id));
        }
        [AllowAnonymous]
        [HttpGet("GetLeadSourceNamesByChannelID/{id}")]
        public async Task<IActionResult> GetLeadSourceNamesByChannelID(long id)
        {
            return await GetResponseHandler(async () => await service.GetLeadSourceNamesByChannelID(id));
        }


        //Create
        [AllowAnonymous]
        [HttpPost("CreateChannel")]
        public async Task<IActionResult> CreateChannel(ChannelCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateChannel(model));
        }
        [AllowAnonymous]
        [HttpPost("CreateLeadType")]
        public async Task<IActionResult> CreateLeadType(ChannelCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateLeadType(model));
        }
        [AllowAnonymous]
        [HttpPost("CreateLeadName")]
        public async Task<IActionResult> CreateLeadName(ChannelCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateLeadName(model));
        }


        //Update
        [AllowAnonymous]
        [HttpPost("UpdateChannel")]
        public async Task<IActionResult> UpdateChannel(ChannelCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.UpdateChannel(model));
        }
        [AllowAnonymous]
        [HttpPost("UpdateLeadSourceType")]
        public async Task<IActionResult> UpdateLeadSourceType(ChannelCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.UpdateLeadSourceType(model));
        }
        [AllowAnonymous]
        [HttpPost("UpdateLeadSourceName")]
        public async Task<IActionResult> UpdateLeadSourceName(ChannelCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.UpdateLeadSourceName(model));
        }
    }
}
