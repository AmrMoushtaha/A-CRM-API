using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class CreateActivityModel

    {
        public long ProcessFlowID { get; set; }

        public long ActivityTypeID { get; set; }

        public string CreatedBy { get; set; }

    }


}
