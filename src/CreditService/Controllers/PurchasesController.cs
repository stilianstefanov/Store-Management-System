using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    using Data.ViewModels.PurchaseProduct;
    using Services.Contracts;
    using static Common.ExceptionMessages;

    [Route("api/borrowers/{borrowerId}/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IBorrowerService _borrowerService;

        public PurchasesController(IPurchaseService purchaseService, IBorrowerService borrowerService)
        {
            _purchaseService = purchaseService;
            _borrowerService = borrowerService;
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
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase(string borrowerId, [FromBody] IEnumerable<PurchaseProductCreateModel> purchasedProducts)
        {
            try
            {
                var borrowerExists = await _borrowerService.BorrowerExistsAsync(borrowerId);

                if (!borrowerExists)
                {
                    return NotFound(BorrowerNotFound);
                }

                var newPurchase = await _purchaseService.CreatePurchaseAsync(borrowerId, purchasedProducts);

                return CreatedAtAction(nameof(GetPurchaseById), new { newPurchase.Id }, newPurchase);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
