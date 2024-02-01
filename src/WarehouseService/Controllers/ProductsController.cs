using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Controllers
{
    using Services.Contracts;
    using static Common.ExceptionMessages;

    [Route("api/warehouses/{warehouseId}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;

        public ProductsController(IProductService productService, IWarehouseService warehouseService)
        {
            _productService = productService;
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByWarehouseIdAsync(string warehouseId)
        {
            try
            {
                var warehouseExists = await _warehouseService.ExistsAsync(warehouseId);

                if (!warehouseExists)
                {
                    return NotFound(WarehouseNotFound);
                }

                var products = await _productService.GetProductsByWarehouseIdAsync(warehouseId);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
