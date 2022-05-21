using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CustomerRequestInputOption
    {
        public long ID { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public long InputID { get; set; }

        [ForeignKey("InputID")]
        public CustomerRequestInput CustomerRequestInput { get; set; }

    }

}
