namespace GMVService.Data.Contracts
{
    using Models;

    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetAllAsync(string userId);

        Task AddAsync(Transaction transaction);
    }
}
