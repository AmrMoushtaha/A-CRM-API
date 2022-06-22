using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class ContactComment
    {
        public long ID { get; set; }

        public string Comment { get; set; }

        public long ContactID { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("ContactID")]
        public virtual Contact Contact { get; set; }

    }

}
