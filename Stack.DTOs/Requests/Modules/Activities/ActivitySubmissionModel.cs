using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class ActivitySubmissionModel

    {
        public long ActivityID { get; set; }

        public string CurrentStage { get; set; }

        public string NewStage { get; set; }

        public string CurrentStatus { get; set; }

        public string NewStatus { get; set; }

        public bool IsStatusChanged { get; set; }

        public string Comment { get; set; }

        public long ScheduledActivityID { get; set; }

    }


}
