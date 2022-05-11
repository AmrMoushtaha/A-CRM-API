using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class CustomerComment
    {
        public long ID { get; set; }

        public string Comment { get; set; }

        public long CustomerID { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

    }

}
