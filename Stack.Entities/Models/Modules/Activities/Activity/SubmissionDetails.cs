using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class SubmissionDetails
    {
        public long ID { get; set; }

        public string CurrentStage { get; set; }

        public string NewStage { get; set; }

        public long? CurrentStatus { get; set; }

        public long? NewStatus { get; set; }

        public bool IsStatusChanged { get; set; }

        public bool IsStageChanged { get; set; }

        public string Comment { get; set; }

        public DateTime? SubmissionDate { get; set; }

        public DateTime? ScheduledActivityDate { get; set; }

        public long? ScheduledActivityID { get; set; }

        [ForeignKey("ScheduledActivityID")]
        public virtual ActivityType ScheduledActivity { get; set; }

        public long ActivityID { get; set; }

        [ForeignKey("ActivityID")]

        public virtual Activity Activity { get; set; }

    }

}
