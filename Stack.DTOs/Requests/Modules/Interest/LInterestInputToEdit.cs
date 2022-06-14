using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.DTOs.Requests.Modules.Interest
{
    public class LInterestInputToEdit
    {
        public long ID { get; set; }
        public string Attachment { get; set; }
        public long? SelectedAttributeID { get; set; }
        public long InputID { get; set; }
        public long InputType { get; set; }
        public long PredefinedInputType { get; set; }

    }

    public class LInterestInputsToEdit
    {
        public List<LInterestInputToEdit> LInterestInputs { get; set; }

    }
}
