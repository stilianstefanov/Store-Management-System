namespace CreditService.Data.Repositories.Contracts
{
    using Models;

    public interface IPurchaseProductRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<PurchaseProduct>> GetProductsByPurchaseIdAsync(string purchaseId);

        Task DeleteProductByIdAsync(string id);
    }
}
