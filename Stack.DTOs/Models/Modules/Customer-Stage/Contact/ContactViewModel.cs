using Stack.DTOs.Models.Modules.Pool;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CustomerStage
{
    public class ContactViewModel
    {
        public long ID { get; set; }

        public string FullNameEN { get; set; }
        public string FullNameAR { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string LeadSourceType { get; set; }
        public string LeadSourceName { get; set; }
        public string PrimaryPhoneNumber { get; set; }

        public string Status { get; set; }
        public string AssignedUserName { get; set; }

        public List<ContactPhoneNumberDTO> ContactPhoneNumbers { get; set; }
    }

    public class ContactPhoneNumberDTO
    {
        public long ID { get; set; }
        public string Number { get; set; }
    }

}
