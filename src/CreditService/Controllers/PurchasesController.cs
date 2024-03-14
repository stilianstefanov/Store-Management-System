namespace CreditService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Utilities.Extensions;
    using Data.ViewModels.PurchasedProduct;
    using Services.Contracts;

    [Route("api/clients/{clientId}/[controller]")]
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
        public async Task<IActionResult> GetPurchasesByClientId(string clientId)
        {
            var result = await _purchaseService.GetPurchasesByClientIdAsync(clientId);

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
        public async Task<IActionResult> CreatePurchase(string clientId,
            [FromBody] IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _purchaseService.CreatePurchaseAsync(clientId, purchasedProducts);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return CreatedAtRoute("GetPurchaseById", new { clientId = clientId, id = result.Data!.Id }, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(string clientId, string id)
        {
            var result = await _purchaseService.DeletePurchaseAsync(id, clientId);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return NoContent();
        }
    }
}
