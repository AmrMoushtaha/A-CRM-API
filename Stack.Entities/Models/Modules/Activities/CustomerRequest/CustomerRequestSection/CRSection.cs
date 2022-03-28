using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CRSection
    {
        public long ID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public int Order { get; set; }

        public bool HasDecisionalQuestions { get; set; }

        //Navigation Properties
        public long TypeID { get; set; }

        [ForeignKey("RequestTypeID")]
        public virtual CRType RequestType { get; set; }

        public virtual List<CR_Section> RequestSections { get; set; }

        public virtual List<CRSectionQuestion> Questions { get; set; }

    }

}
