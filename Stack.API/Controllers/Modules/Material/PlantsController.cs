using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Requests.Modules.Materials.Plants;
using Stack.DTOs.Requests.Shared;
using Stack.ServiceLayer.Modules.Materials;
using System.Threading.Tasks;

namespace Stack.API.Controllers.Modules.Organizations
{
    [Route("api/Plants")]
    [ApiController]

    public class PlantsController : BaseResultHandlerController<PlantsService>
    {
        public PlantsController(PlantsService _service) : base(_service)
        {

        }

        [HttpGet("GetAllPlants")]
        public async Task<IActionResult> GetAllPlants()
        {
            return await GetResponseHandler(async () => await service.GetAllPlants());
        }

        [HttpPost("CreatePlant")]
        public async Task<IActionResult> CreatePlant(PlantCreationModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreatePlant(model));
        }


        [HttpPost("EditPlant")]
        public async Task<IActionResult> EditPlant(EditPlantModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditPlant(model));
        }

        [HttpPost("DeletePlant")]
        public async Task<IActionResult> DeletePlant(DeleteRecordModel model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeletePlant(model));
        }


    }

}
