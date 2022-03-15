using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class CreateActivityTypeSectionModel

    {
        public string  NameAR { get; set; }

        public string NameEN { get; set; }

        public string Type { get; set; }

        public long RoutesTo { get; set; }

        public long ActivityTypeID { get; set; }


    }


}
