namespace WarehouseService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.ViewModels;
    using Services.Contracts;
    using Utilities.Extensions;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var result = await _warehouseService.GetAllAsync(User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpGet("{id}", Name = "GetWarehouseById")]
        public async Task<IActionResult> GetWarehouseById(string id)
        {
            var result = await _warehouseService.GetByIdAsync(id, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouse(WarehouseReadModel model)
        {
            var result = await _warehouseService.CreateAsync(model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return CreatedAtRoute("GetWarehouseById", new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(string id, [FromBody]WarehouseReadModel model)
        {
            var result = await _warehouseService.UpdateAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }
    }
}
