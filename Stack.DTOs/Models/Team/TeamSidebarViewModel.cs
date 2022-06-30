using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Teams
{
    public class TeamSidebarViewModel
    {
        public long ID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public int MembersCount { get; set; }

    }


}
