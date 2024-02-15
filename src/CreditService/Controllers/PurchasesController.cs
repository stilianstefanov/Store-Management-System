using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    using Data.ViewModels.PurchasedProduct;
    using Services.Contracts;
    using static Common.ExceptionMessages;

    [Route("api/borrowers/{borrowerId}/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IBorrowerService _borrowerService;
        private readonly IPurchasedProductService _purchasedProductService;

        public PurchasesController(
            IPurchaseService purchaseService,
            IBorrowerService borrowerService,
            IPurchasedProductService purchasedProductService)
        {
            _purchaseService = purchaseService;
            _borrowerService = borrowerService;
            _purchasedProductService = purchasedProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchasesByBorrowerId(string borrowerId)
        {
            try
            {
                var borrowerExists = await _borrowerService.BorrowerExistsAsync(borrowerId);

                if (!borrowerExists)
                {
                    return NotFound(BorrowerNotFound);
                }

                var purchases = await _purchaseService.GetPurchasesByBorrowerIdAsync(borrowerId);

                return Ok(purchases);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetPurchaseById")]
        public async Task<IActionResult> GetPurchaseById(string id)
        {
            try
            {
                return Ok(await _purchaseService.GetPurchaseByIdAsync(id));
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

        [HttpPost]
        public async Task<IActionResult> CreatePurchase(string borrowerId, [FromBody] IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var borrowerExists = await _borrowerService.BorrowerExistsAsync(borrowerId);

                if (!borrowerExists)
                {
                    return NotFound(BorrowerNotFound);
                }

                var productsExist = await _purchasedProductService.ValidateProductsAsync(purchasedProducts);

                if (!productsExist)
                {
                    return BadRequest(ProductsNotFound);
                }

                var newPurchase = await _purchaseService.CreatePurchaseAsync(borrowerId, purchasedProducts);

                var isSucceeded = await _borrowerService.IncreaseBorrowerCreditAsync(borrowerId, newPurchase.Amount);

                if (!isSucceeded)
                {
                    return BadRequest(InsufficientCredit);
                }

                await _purchaseService.CompletePurchaseAsync();

                return CreatedAtAction(nameof(GetPurchaseById), new { borrowerId, newPurchase.Id }, newPurchase);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(string borrowerId, string id)
        {
            try
            {
                var borrowerExists = await _borrowerService.BorrowerExistsAsync(borrowerId);

                if (!borrowerExists)
                {
                    return NotFound(BorrowerNotFound);
                }

                var totalAmount = await _purchaseService.DeletePurchaseAsync(id);

                await _borrowerService.DecreaseBorrowerCreditAsync(borrowerId, totalAmount);

                await _purchasedProductService.DeleteBoughtProductsByPurchaseIdAsync(id);

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
