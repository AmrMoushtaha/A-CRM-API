using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CustomerRequestInput
    {
        public long ID { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public long RequestID { get; set; }

        public int Type { get; set; }

        [ForeignKey("RequestID")]
        public CustomerRequest CustomerRequest { get; set; }

        public List<CustomerRequestInputOption> Options { get; set; }

        //public List<CustomerRequestInputAnswer> Answers { get; set; }

    }

}
