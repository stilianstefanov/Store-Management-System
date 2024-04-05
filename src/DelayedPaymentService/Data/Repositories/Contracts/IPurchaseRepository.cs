namespace DelayedPaymentService.Data.Repositories.Contracts
{
    using Models;

    public interface IPurchaseRepository
    {
        Task SaveChangesAsync();

        IQueryable<Purchase> GetPurchasesByClientIdAsync(string clientId);

        Task<Purchase?> GetPurchaseByIdAsync(string id);

        Task AddPurchaseAsync(Purchase purchase);

        Task<decimal> DeletePurchaseAsync(string id);

        Task<bool> PurchaseExistsAsync(string id);
    }
}
