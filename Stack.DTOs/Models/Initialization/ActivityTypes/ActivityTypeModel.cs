using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Initialization.ActivityTypes
{
    public class ActivityTypeModel
    {
        public string NameAR { get; set; }

        public string NameEN { get; set; }

        public string Status { get; set; }


        public string ColorCode { get; set; }

        public List<SectionModel> Sections { get; set; }


    }

}
