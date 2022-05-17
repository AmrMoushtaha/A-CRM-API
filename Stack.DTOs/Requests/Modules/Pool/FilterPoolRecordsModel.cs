using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class FilterPoolRecordsModel
    {
        public long? PoolID { get; set; }
        public string FullNameEN { get; set; }
        public string FullNameAR { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long ChannelID { get; set; }
        public long LSTID { get; set; }
        public long LSNID { get; set; }
        public int CustomerStage { get; set; }
        public DateTime? LastActivityDate_RangeMin { get; set; }
        public DateTime? LastActivityDate_RangeMax { get; set; }
        public DateTime? NextActionDate_RangeMin { get; set; }
        public DateTime? NextActionDate_RangeMax { get; set; }
    }

}
