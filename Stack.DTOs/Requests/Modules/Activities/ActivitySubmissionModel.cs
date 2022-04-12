using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class ActivitySubmissionModel
    {
        public long ActivityID { get; set; }

        public long RecordID { get; set; }

        public string CurrentStage { get; set; }

        public string NewStage { get; set; }

        public long NewStatusID { get; set; }

        public string Comment { get; set; }

        public long ScheduledActivityTypeID { get; set; }

        public long ScheduledActivityDate { get; set; }

    }


}
