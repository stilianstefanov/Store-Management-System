namespace GMVService.Data
{
    using Contracts;
    using Models;

    public class TransactionRepository : ITransactionRepository
    {
        public IQueryable<Transaction> GetAllAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
