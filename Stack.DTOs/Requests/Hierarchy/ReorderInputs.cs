using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Stack.DTOs.Requests.Modules.Hierarchy
{
    public class ReorderInputs
    {
        public ReorderInput FirstInput { get; set; }
        public ReorderInput SecondInput { get; set; }


    }

}
