namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchasedProduct;

    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseViewModel>> GetPurchasesByBorrowerIdAsync(string borrowerId);

        Task<PurchaseViewModel> GetPurchaseByIdAsync(string id);
        
        Task<PurchaseViewModel> CreatePurchaseAsync(string borrowerId, IEnumerable<PurchasedProductCreateModel> purchasedProducts);
        
        Task<decimal> DeletePurchaseAsync(string id);

        Task DeletePurchasesByBorrowerIdAsync(string borrowerId);

        Task CompletePurchaseAsync();

        Task<bool> PurchaseExistsAsync(string id);
    }
}
