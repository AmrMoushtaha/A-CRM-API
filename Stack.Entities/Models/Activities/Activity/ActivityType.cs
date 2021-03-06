using System.Collections.Generic;

namespace Stack.Entities.Models.Modules.Activities
{
    public class ActivityType
    {
        public long ID { get; set; }

        public string NameAR { get; set; }

        public string NameEN { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }


        public string ColorCode { get; set; }

        //Navigation Properties . 

        public virtual List<Activity> Activities { get; set; }

        public virtual List<SubmissionDetails> SubmissionDetails { get; set; }

        public virtual List<Section> Sections { get; set; }

    }

}
