namespace CreditService.Data.Repositories.Contracts
{
    using Models;

    public interface IBorrowerRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Borrower>> GetAllBorrowersAsync(string userId);

        Task<Borrower?> GetBorrowerByIdAsync(string id);

        Task AddBorrowerAsync(Borrower borrower);

        Task DeleteBorrowerAsync(string id);

        Task<bool> BorrowerExistsAsync(string id);
    }
}
