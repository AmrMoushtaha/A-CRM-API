using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class PoolViewModel
    {
        public long ID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public  List<ContactListViewModel> Contacts { get; set; }
    }
}
