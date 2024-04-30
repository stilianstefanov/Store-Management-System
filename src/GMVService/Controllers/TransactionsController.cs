using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMVService.Controllers
{
    using Data.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Services.Contracts;
    using Utilities.Extensions;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGmvDetails([FromQuery] TransactionsQueryModel queryModel)
        {
            var result = await _transactionService.GetGmvDetailsAsync(User.GetId()!, queryModel);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }
    }
}
