
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Hierarchy
{
    public class SectionToEdit
    {
        public long ID { get; set; }

        public string LabelEN { get; set; }
        public string LabelAR { get; set; }

        public long LevelID { get; set; }

    }

}
