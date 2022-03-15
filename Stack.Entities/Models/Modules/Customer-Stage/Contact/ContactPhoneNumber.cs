using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class ContactPhoneNumber
    {
        public long ID { get; set; }

        public virtual string Number { get; set; }

        public long ContactID { get; set; }

        [ForeignKey("ContactID")]
        public virtual Contact Contact { get; set; }

    }

}
