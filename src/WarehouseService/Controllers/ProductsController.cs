namespace WarehouseService.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Contracts;
    using Utilities;
    using Utilities.Enums;

    [Route("api/warehouses/{warehouseId}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByWarehouseId(string warehouseId)
        {
            var result = await _productService.GetProductsByWarehouseIdAsync(warehouseId);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.ErrorMessage),
                _ =>  this.GeneralError()
                };
            }

            return Ok(result.Data);
        }
    }
}
