namespace GMVService.Data.Contracts
{
    using Models;

    public interface ITransactionRepository
    {
        Task SaveChangesAsync();

        IQueryable<Transaction> GetAllAsync(string userId);

        Task AddAsync(Transaction transaction);
    }
}
