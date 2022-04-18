using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class LInterestInputToEdit
    {
        public long ID { get; set; }
        public long LInterestID { get; set; }
        public long LInterestInputID { get; set; }

    }

}
