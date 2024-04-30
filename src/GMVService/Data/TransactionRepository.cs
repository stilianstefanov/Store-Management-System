namespace GMVService.Data
{
    using Contracts;
    using Models;

    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Transaction> GetAllAsync(string userId)
        {
            return _dbContext.Transactions
                .Where(t => t.UserId == userId);
        }

        public async Task AddAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
