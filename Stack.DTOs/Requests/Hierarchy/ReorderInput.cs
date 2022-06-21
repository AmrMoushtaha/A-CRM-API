using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Stack.DTOs.Requests.Modules.Hierarchy
{
    public class ReorderInput
    {
        public long ID { get; set; }
        public int Order { get; set; }

    }

}
