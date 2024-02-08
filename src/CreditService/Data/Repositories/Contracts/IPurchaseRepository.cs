namespace CreditService.Data.Repositories.Contracts
{
    using Models;

    public interface IPurchaseRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Purchase>> GetPurchasesByBorrowerIdAsync(string borrowerId);

        Task AddPurchaseAsync(Purchase purchase);

        Task DeletePurchaseAsync(string id);
    }
}
