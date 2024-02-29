﻿namespace CreditService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.Contracts;
    using Utilities.Extensions;

    [Route("api/borrowers/{borrowerId}/purchases/{purchaseId}/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchasedProductsController : ControllerBase
    {
        private readonly IPurchasedProductService _purchaseProductService;

        public PurchasedProductsController(IPurchasedProductService purchaseProductService)
        {
            _purchaseProductService = purchaseProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoughtProductsByPurchaseId(string purchaseId)
        {
            var result = await _purchaseProductService.GetBoughtProductsByPurchaseIdAsync(purchaseId);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoughtProductById(string borrowerId, string id)
        {
            var result = await _purchaseProductService.DeleteBoughtProductByIdAsync(borrowerId, id);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return NoContent();
        }
    }
}
