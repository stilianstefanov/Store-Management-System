﻿namespace WarehouseService.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Contracts;
    using Utilities.Extensions;
    using Data.ViewModels;

    [Route("api/warehouses/{warehouseId}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByWarehouseId(string warehouseId, [FromQuery] ProductsAllQueryModel queryModel)
        {
            var result = await _productService.GetProductsByWarehouseIdAsync(warehouseId, queryModel);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }
    }
}
