namespace GMVService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface ITransactionService
    {
        Task<OperationResult<TransactionsQueryModel>> GetGmvAsync(string userId, TransactionsQueryModel queryModel);
    }
}
