using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities.ActivityDetails
{
    public class ActivityDetailsDTO
    {

        public string NameEN { get; set; }

        public string NameAR { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PerformedBy { get; set; }

        public string Comment { get; set; }

        public DateTime ScheduledActivityDate { get; set; }

        public string ScheduledActivityNameEN { get; set; }

        public string ScheduledActivityNameAR { get; set; }

        public bool IsStatusChanged { get; set; }

        public bool IsStageChanged { get; set; }

        public string Stage { get; set; } 

        public string NewStage { get; set; }


        public string StatusEN { get; set; }
        public string StatusAR { get; set; }


        public string NewStatusEN { get; set; }
        public string NewStatusAR { get; set; }


        public List<ActivitySectionDetailsDTO> ActivitySections {get;set;}

    }

}
