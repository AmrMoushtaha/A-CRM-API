using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class CustomerPhoneNumber
    {
        public long ID { get; set; }

        public virtual string Number { get; set; }

        public long CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

    }

}
