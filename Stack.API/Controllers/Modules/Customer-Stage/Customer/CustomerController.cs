using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Auth;
using Stack.ServiceLayer.Modules.Auth;
using Stack.ServiceLayer.Modules.CustomerStage;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Auth
{
    [Route("api/Customer")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class CustomerController : BaseResultHandlerController<CustomerService>
    {
        public CustomerController(CustomerService _service) : base(_service)
        {

        }

  
    }

}
