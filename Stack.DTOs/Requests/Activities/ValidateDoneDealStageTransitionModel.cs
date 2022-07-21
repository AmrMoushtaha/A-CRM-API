using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class ValidateDoneDealStageTransitionModel
    {
        public long ReferenceID { get; set; }

        public bool isCustomer { get; set; }
    }


}
