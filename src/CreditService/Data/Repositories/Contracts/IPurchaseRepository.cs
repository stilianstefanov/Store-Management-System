namespace CreditService.Data.Repositories.Contracts
{
    using Models;

    public interface IPurchaseRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Purchase>> GetPurchasesByBorrowerIdAsync(string borrowerId);

        Task<Purchase> GetPurchaseByIdAsync(string id);

        Task AddPurchaseAsync(Purchase purchase);

        Task<decimal> DeletePurchaseAsync(string id);
    }
}
