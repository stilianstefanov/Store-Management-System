using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    using Data.ViewModels.PurchasedProduct;
    using Microsoft.AspNetCore.Authorization;
    using Services.Contracts;
    using Utilities;

    [Route("api/borrowers/{borrowerId}/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchasesByBorrowerId(string borrowerId)
        {
            var result = await _purchaseService.GetPurchasesByBorrowerIdAsync(borrowerId);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpGet("{id}", Name = "GetPurchaseById")]
        public async Task<IActionResult> GetPurchaseById(string id)
        {
            var result = await _purchaseService.GetPurchaseByIdAsync(id);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase(string borrowerId,
            [FromBody] IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _purchaseService.CreatePurchaseAsync(borrowerId, purchasedProducts);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return CreatedAtRoute("GetPurchaseById", new { borrowerId = borrowerId, id = result.Data!.Id }, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(string borrowerId, string id)
        {
            var result = await _purchaseService.DeletePurchaseAsync(id, borrowerId);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return NoContent();
        }
    }
}
