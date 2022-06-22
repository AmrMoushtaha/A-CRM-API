using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class CustomerRequestCreationModel
    {
        public long RequestTypeID { get; set; }
        public long RecordID { get; set; }
        public long? InterestID { get; set; }
        public int CustomerStage { get; set; }

        public int TypeIndex { get; set; }
        public List<CustomerRequestPhaseFulfillmentModel> Phases { get; set; }
    }


    public class CustomerRequestPhaseFulfillmentModel
    {
        public long ID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<CustomerRequestPhaseInputsFulfillmentModel> Inputs { get; set; }

    }

    public class CustomerRequestPhaseInputsFulfillmentModel
    {
        public long ID { get; set; }
        public string Answer { get; set; }

    }
}
