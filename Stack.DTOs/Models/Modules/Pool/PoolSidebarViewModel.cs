using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class PoolSidebarViewModel
    {
        public string? NameAR { get; set; }
        public string? NameEN { get; set; }

        public List<ContactSidebarViewModel>? Contacts { get; set; }

    }


    public class ContactSidebarViewModel
    {
        public string? NameAR { get; set; }
        public string? NameEN { get; set; }
    }

}
