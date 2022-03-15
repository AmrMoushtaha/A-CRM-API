using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Shared
{
    public class CreationTemplate
    {
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }
    }

}
