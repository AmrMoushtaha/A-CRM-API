using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class Section
    {
        public long ID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string Type { get; set; }
        public long RoutesTo { get; set; }


        //Navigation Properties
        public long ActivityTypeID { get; set; }

        [ForeignKey("ActivityTypeID")]
        public virtual ActivityType ActivityType { get; set; }

        public virtual List<ActivitySection> ActivitySections { get; set; }

        public virtual List<SectionQuestion> Questions { get; set; }
    }

}
