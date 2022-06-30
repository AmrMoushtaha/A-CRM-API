
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Models.Modules.Pool;
using Stack.DTOs.Models.Modules.Teams;
using Stack.DTOs.Requests.Modules.Teams;
using Stack.Entities.Enums.Modules.Teams;
using Stack.Entities.Models.Modules.Teams;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stack.ServiceLayer.Modules.Teams
{
    public class TeamService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private static readonly HttpClient client = new HttpClient();

        public TeamService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.mapper = mapper;

        }

        public async Task<ApiResponse<List<TeamSidebarViewModel>>> GetAllTeams()
        {
            ApiResponse<List<TeamSidebarViewModel>> result = new ApiResponse<List<TeamSidebarViewModel>>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {

                    var teams = await unitOfWork.TeamManager.GetAllTeams();

                    if (teams != null && teams.Count > 0)
                    {
                        result.Succeeded = true;
                        result.Data = teams;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("No pools found");
                        result.Errors.Add("لا يوجد قوائم");
                        result.ErrorType = ErrorType.NotFound;
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }
        public async Task<ApiResponse<List<TeamMemberViewModel>>> GetTeamMembers(long teamID)
        {
            ApiResponse<List<TeamMemberViewModel>> result = new ApiResponse<List<TeamMemberViewModel>>();
            try
            {


                var teams = await unitOfWork.TeamUserManager.GetTeamMembers(teamID);

                if (teams != null && teams.Count > 0)
                {
                    result.Succeeded = true;
                    result.Data = teams;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No pools found");
                    result.Errors.Add("لا يوجد قوائم");
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> CreateTeam(TeamCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var userID = await HelperFunctions.GetUserID(_httpContextAccessor);

                if (userID != null)
                {

                    //check team's existence

                    var teamExistsQ = await unitOfWork.TeamManager.GetAsync(t => t.NameEN == model.NameEN || t.NameAR == model.NameAR);
                    var teamExists = teamExistsQ.FirstOrDefault();

                    if (teamExists == null)
                    {
                        //Create team

                        Team team = new Team
                        {
                            NameEN = model.NameEN,
                            NameAR = model.NameAR,
                            DescriptionEN = model.DescriptionEN,
                            DescriptionAR = model.DescriptionAR,
                            ParentTeamID = model.ParentTeamID,
                        };

                        var teamCreationRes = await unitOfWork.TeamManager.CreateAsync(team);

                        if (teamCreationRes != null)
                        {
                            await unitOfWork.SaveChangesAsync();

                            //create team members

                            if (model.TeamMembers != null && model.TeamMembers.Count > 0)
                            {
                                for (int i = 0; i < model.TeamMembers.Count; i++)
                                {
                                    var teamMember = model.TeamMembers[i];

                                    Team_User teamMemberCreationModel = new Team_User
                                    {
                                        UserID = teamMember.UserID,
                                        JoinDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                                        IsManager = teamMember.isManager,
                                        Status = (int)TeamMemberStatuses.Active,
                                        TeamID = teamCreationRes.ID,
                                    };

                                    var memberCreationRes = await unitOfWork.TeamUserManager.CreateAsync(teamMemberCreationModel);
                                }

                                await unitOfWork.SaveChangesAsync();
                            }

                            result.Succeeded = true;
                            result.Data = true;
                            return result;

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Team creation failed");
                            result.Errors.Add("Team creation failed");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("A team with such name already exists, please choose a different name");
                        result.Errors.Add("A team with such name already exists, please choose a different name");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Not authorized");
                    result.Errors.Add("غير مصرح");
                    result.ErrorCode = ErrorCode.A500;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }
        public async Task<ApiResponse<bool>> AddTeamMember(TeamMemberCreationModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                //check team's existence

                var teamMemberExistsQ = await unitOfWork.TeamUserManager.GetAsync(t => t.UserID == model.UserID);
                var teamMemberExists = teamMemberExistsQ.FirstOrDefault();

                if (teamMemberExists == null)
                {

                    //create team members

                    Team_User teamMemberCreationModel = new Team_User
                    {
                        UserID = model.UserID,
                        JoinDate = await HelperFunctions.GetEgyptsCurrentLocalTime(),
                        IsManager = model.isManager,
                        Status = (int)TeamMemberStatuses.Active,
                        TeamID = model.TeamID.Value,
                    };

                    var memberCreationRes = await unitOfWork.TeamUserManager.CreateAsync(teamMemberCreationModel);

                    if (memberCreationRes != null)
                    {
                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error adding team member");
                        result.Errors.Add("Error adding team member");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("This user is already in a team");
                    result.Errors.Add("This user is already in a team");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }


        public async Task<ApiResponse<bool>> ChangeMemberStatus(string memberID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {


                var teamMemberQ = await unitOfWork.TeamUserManager.GetAsync(t => t.UserID == memberID);
                var teamMember = teamMemberQ.FirstOrDefault();

                if (teamMember != null)
                {


                    if (teamMember.Status == (int)TeamMemberStatuses.Active)
                    {
                        teamMember.Status = (int)TeamMemberStatuses.Suspended;
                    }
                    else
                    {
                        teamMember.Status = (int)TeamMemberStatuses.Active;
                    }

                    var updateRes = await unitOfWork.TeamUserManager.UpdateAsync(teamMember);

                    if (updateRes)
                    {
                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating team member");
                        result.Errors.Add("Error updating team member");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Team member not found");
                    result.Errors.Add("Team member not found");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }
    }

}