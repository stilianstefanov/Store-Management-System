namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchaseProduct;

    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseViewModel>> GetPurchasesByBorrowerIdAsync(string borrowerId);

        Task<PurchaseViewModel> GetPurchaseByIdAsync(string id);
        
        Task<PurchaseViewModel> CreatePurchaseAsync(string borrowerId, IEnumerable<PurchaseProductCreateModel> purchasedProducts);
        
        Task DeletePurchaseAsync(string id);
    }
}
