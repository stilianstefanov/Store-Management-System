namespace GMVService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface ITransactionService
    {
        Task<OperationResult<TransactionsQueryModel>> GetGmvDetailsAsync(string userId, TransactionsQueryModel queryModel);
    }
}
