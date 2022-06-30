using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Teams
{
    public class TeamCreationModel
    {
        public long? ParentTeamID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public List<TeamMemberCreationModel> TeamMembers { get; set; }
    }


    public class TeamMemberCreationModel
    {
        public long? TeamID { get; set; }
        public string UserID { get; set; }
        public bool IsManager { get; set; }
        public string ManagerID { get; set; }

    }

}
