using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Entities.Models
{
    public class BaseEntity
    {
        public long ID { get; set; }
        //public int? CreatedBy { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
