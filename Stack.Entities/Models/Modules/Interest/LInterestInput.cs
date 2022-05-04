using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Hierarchy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Interest
{
    public class LInterestInput : BaseEntity
    {
        public string Attachment { get; set; }

        // input properties
        public string InputField { get; set; }
        public string InputType { get; set; }
        public long AttributeID { get; set; }

        public long InputID { get; set; }
        [ForeignKey("InputID")]
        public virtual Input Input { get; set; }
    }

}
