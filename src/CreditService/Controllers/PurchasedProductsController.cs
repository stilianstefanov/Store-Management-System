using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    using Services.Contracts;
    using static Common.ExceptionMessages;

    [Route("api/borrowers/{borrowerId}/purchases/{purchaseId}/[controller]")]
    [ApiController]
    public class PurchasedProductsController : ControllerBase
    {
        private readonly IPurchasedProductService _purchaseProductService;
        private readonly IBorrowerService _borrowerService;
        private readonly IPurchaseService _purchaseService;

        public PurchasedProductsController(
            IPurchasedProductService purchaseProductService,
            IBorrowerService borrowerService,
            IPurchaseService purchaseService)
        {
            _purchaseProductService = purchaseProductService;
            _borrowerService = borrowerService;
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoughtProductsByPurchaseId(string borrowerId, string purchaseId)
        {
            try
            {
                var borrowerExists = await _borrowerService.BorrowerExistsAsync(borrowerId);

                if (!borrowerExists)
                {
                    return NotFound(BorrowerNotFound);
                }

                var purchaseExists = await _purchaseService.PurchaseExistsAsync(purchaseId);

                if (!purchaseExists)
                {
                    return NotFound(PurchaseNotFound);
                }

                var products = await _purchaseProductService.GetBoughtProductsByPurchaseIdAsync(purchaseId);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoughtProductById(string borrowerId, string id)
        {
            try
            {
                var borrowerExists = await _borrowerService.BorrowerExistsAsync(borrowerId);

                if (!borrowerExists)
                {
                    return NotFound(BorrowerNotFound);
                }

                var amount = await _purchaseProductService.DeleteBoughtProductByIdAsync(id);

                await _borrowerService.DecreaseBorrowerCreditAsync(borrowerId, amount);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
