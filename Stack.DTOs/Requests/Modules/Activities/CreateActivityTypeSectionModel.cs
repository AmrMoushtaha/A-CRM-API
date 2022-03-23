using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class CreateActivityTypeSectionModel

    {
        public string  NameAR { get; set; }

        public string NameEN { get; set; }

        public int Order { get; set; }

        public long ActivityTypeID { get; set; }

        public bool HasCreateInterest { get; set; }

        public bool HasCreateRequest { get; set; }

        public bool HasCreateResale { get; set; }


    }


}
