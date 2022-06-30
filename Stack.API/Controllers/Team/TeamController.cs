using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Requests.CustomerStage;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.DTOs.Requests.Modules.Pool;
using Stack.DTOs.Requests.Modules.Teams;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.pool;
using Stack.ServiceLayer.Modules.Teams;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Teams
{
    [Route("api/Teams")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class TeamController : BaseResultHandlerController<TeamService>
    {
        public TeamController(TeamService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpGet("GetAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
            return await GetResponseHandler(async () => await service.GetAllTeams());
        }

        [AllowAnonymous]
        [HttpGet("GetTeamApplicableSystemUsers")]
        public async Task<IActionResult> GetTeamApplicableSystemUsers()
        {
            return await GetResponseHandler(async () => await service.GetTeamApplicableSystemUsers());
        }

        [AllowAnonymous]
        [HttpGet("GetTeamMembers/{teamID}")]
        public async Task<IActionResult> GetTeamMembers(long teamID)
        {
            return await GetResponseHandler(async () => await service.GetTeamMembers(teamID));
        }

        [AllowAnonymous]
        [HttpPost("CreateTeam")]
        public async Task<IActionResult> CreateTeam(TeamCreationModel model)
        {
            return await GetResponseHandler(async () => await service.CreateTeam(model));
        }

        [AllowAnonymous]
        [HttpPost("AddTeamMember")]
        public async Task<IActionResult> AddTeamMember(TeamMemberCreationModel model)
        {
            return await GetResponseHandler(async () => await service.AddTeamMember(model));
        }


        [AllowAnonymous]
        [HttpGet("ChangeMemberStatus/{memberID}")]
        public async Task<IActionResult> ChangeMemberStatus(string memberID)
        {
            return await GetResponseHandler(async () => await service.ChangeMemberStatus(memberID));
        }
    }
}
