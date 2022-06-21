using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities
{
    public class ActivityHistoryViewDTO
    {
        public long ID { get; set; }

        public long ProcessFlowID { get; set; }

        public long ActivityTypeID { get; set; }

        public string ActivityTypeNameAR { get; set; }
        public string ActivityTypeNameEN { get; set; }

        public string ColorCode { get; set; }

        public DateTime? CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? SubmissionDate { get; set; }

    }

}
