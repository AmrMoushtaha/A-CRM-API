using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Common
{
    public class ConnectionID
    {
        [Key]
        [MaxLength(256)]
        public string ID { get; set; }

        public long PoolID { get; set; }

        public long RecordID { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserID { get; set; }

        [ForeignKey("UserID")]

        public virtual ApplicationUser User { get; set; }

    }

}
