using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class CRUpdatePhaseModel
    {
        public long RequestID { get; set; }
        public long PhaseID { get; set; }
        public int Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<CustomerRequestPhaseInputsFulfillmentModel> Inputs { get; set; }
    }

}
