using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Hierarchy
{
    public class LevelToAdd
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public int Type { get; set; }

    }

}
