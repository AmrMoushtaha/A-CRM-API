using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class LInterestInputToAdd
    {
        public string Attachment { get; set; }
        public long? SelectedAttributeID { get; set; }
        public long InputID { get; set; }  

        //// input properties
        //public string InputField { get; set; }
        //public string InputType { get; set; }
        //public long AttributeID { get; set; }


    }

    public class LInterestInputsToAdd
    {
        public List<LInterestInputToAdd> LInterestInputs{ get; set; }

    }

}
