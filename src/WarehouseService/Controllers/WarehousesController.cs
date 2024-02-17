namespace WarehouseService.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Data.ViewModels;
    using Services.Contracts;
    using Utilities;
    using Utilities.Enums;

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
        public async Task<IActionResult> GetAllWarehouses()
        {
            var result = await _warehouseService.GetAllAsync();

            if (!result.IsSuccess) return this.GeneralError();

            return Ok(result.Data);
        }

        [HttpGet("{id}", Name = "GetWarehouseById")]
        public async Task<IActionResult> GetWarehouseById(string id)
        {
            var result = await _warehouseService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.ErrorMessage),
                    _ => this.GeneralError()
                };
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouse(WarehouseReadModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _warehouseService.CreateAsync(model);

            if (!result.IsSuccess) return this.GeneralError();

            return CreatedAtRoute("GetWarehouseById", new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(string id, [FromBody]WarehouseReadModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _warehouseService.UpdateAsync(id, model);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.ErrorMessage),
                    _ => this.GeneralError()
                };
            }

            return Ok(result.Data);
        }
    }
}
