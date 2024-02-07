namespace CreditService.Data.Repositories.Contracts
{
    using Models;

    public interface IBorrowerRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Borrower>> GetAllBorrowersAsync();

        Task<Borrower> GetBorrowerByIdAsync(string id);

        Task AddBorrowerAsync(Borrower borrower);

        Task<Borrower> UpdateBorrowerAsync(string id, Borrower borrower);

        Task DeleteBorrowerAsync(string id);
    }
}
