using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    using Data.ViewModels.Borrower;
    using Services.Contracts;
    using Utilities;

    [Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowersController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowers()
        {
            var result = await _borrowerService.GetAllBorrowersAsync();

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpGet("{id}", Name = "GetBorrowerById")]
        public async Task<IActionResult> GetBorrowerById(string id)
        {
           var result = await _borrowerService.GetBorrowerByIdAsync(id);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrower(BorrowerCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _borrowerService.CreateBorrowerAsync(model);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return CreatedAtAction(nameof(GetBorrowerById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrower(string id, [FromBody]BorrowerUpdateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _borrowerService.UpdateBorrowerAsync(id, model);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(string id)
        {
            var result = await _borrowerService.DeleteBorrowerAsync(id);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return NoContent();
        }
    }
}
