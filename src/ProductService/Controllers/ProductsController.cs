using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    using Data.ViewModels;
    using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllAsync(User.GetId()!);

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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productService.CreateAsync(model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody]ProductUpdateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _productService.UpdateAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateProduct(string id, [FromBody] ProductPartialUpdateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _productService.PartialUpdateAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
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
