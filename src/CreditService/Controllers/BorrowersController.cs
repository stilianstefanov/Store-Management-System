using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    using Data.ViewModels.Borrower;
    using Services.Contracts;

    [Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;
        private readonly IPurchaseService _purchaseService;

        public BorrowersController(IBorrowerService borrowerService, IPurchaseService purchaseService)
        {
            _borrowerService = borrowerService;
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowers()
        {
            try
            {
                var borrowers = await _borrowerService.GetAllBorrowersAsync();

                return Ok(borrowers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetBorrowerById")]
        public async Task<IActionResult> GetBorrowerById(string id)
        {
            try
            {
                var borrower = await _borrowerService.GetBorrowerByIdAsync(id);

                return Ok(borrower);
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
        public async Task<IActionResult> CreateBorrower(BorrowerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newBorrower = await _borrowerService.CreateBorrowerAsync(model);

                return CreatedAtAction(nameof(GetBorrowerById), new { id = newBorrower.Id }, newBorrower);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrower(string id, [FromBody]BorrowerUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedBorrower = await _borrowerService.UpdateBorrowerAsync(id, model);

                return Ok(updatedBorrower);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(string id)
        {
            try
            {
                await _borrowerService.DeleteBorrowerAsync(id);

                await _purchaseService.DeletePurchasesByBorrowerIdAsync(id);

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
