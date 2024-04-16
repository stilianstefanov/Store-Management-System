namespace ProductService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.ViewModels;
    using Services.Contracts;
    using Utilities.Extensions;

    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllProducts(ProductsAllQueryModel queryModel)
        {
            var result = await _productService.GetAllAsync(User.GetId()!, queryModel);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _productService.GetByIdAsync(id, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);
                
            return Ok(result.Data);
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            var result = await _productService.GetByBarcodeAsync(barcode, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateModel model)
        {
            var result = await _productService.CreateAsync(model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody]ProductUpdateModel model)
        {
            var result = await _productService.UpdateAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateProduct(string id, [FromBody] ProductPartialUpdateModel model)
        {
            var result = await _productService.PartialUpdateAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPatch("decrease-stocks")]
        public async Task<IActionResult> DecreaseStocks([FromBody] IEnumerable<ProductStockUpdateModel> models)
        {
            var result = await _productService.DecreaseStocksAsync(models, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _productService.DeleteAsync(id, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return NoContent();
        }
    }
}
