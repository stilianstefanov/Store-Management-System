using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Controllers
{
    using Data.Contracts;
    using Data.ViewModels;
    using Services.Contracts;

    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehousesController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _warehouseService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(WarehouseCreateModel model)
        {
            await _warehouseService.CreateAsync(model);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
