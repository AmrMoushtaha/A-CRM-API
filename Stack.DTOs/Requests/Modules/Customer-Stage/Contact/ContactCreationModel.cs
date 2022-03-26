using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class ContactCreationModel
    {
        public long PoolID { get; set; }
        public string FullNameEN { get; set; }
        public string FullNameAR { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string LeadSourceType { get; set; }
        public string LeadSourceName { get; set; }
        public string PrimaryPhoneNumber { get; set; }

        public string AssigneeID { get; set; }
        public long? StatusID { get; set; }

        public List<ContactPhoneNumberDTO> ContactPhoneNumbers { get; set; }

    }

}
