using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{


    public class ContactListMenuView
    {
        public int TotalRecords { get; set; }
        public List<ContactListViewModel> Records { get; set; }
    }

    public class ContactListViewModel
    {
        public long ID { get; set; }
        public string FullNameAR { get; set; }
        public string FullNameEN { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public bool IsLocked { get; set; }
        public string AssignedUser { get; set; }

    }
}
