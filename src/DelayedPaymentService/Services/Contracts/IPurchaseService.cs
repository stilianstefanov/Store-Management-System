namespace DelayedPaymentService.Services.Contracts
{
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchasedProduct;
    using Utilities;

    public interface IPurchaseService
    {
        Task<OperationResult<IEnumerable<PurchaseViewModel>>> GetPurchasesByClientIdAsync(string clientId);

        Task<OperationResult<PurchaseViewModel>> GetPurchaseByIdAsync(string id);
        
        Task<OperationResult<PurchaseViewModel>> CreatePurchaseAsync(string clientId, IEnumerable<PurchasedProductCreateModel> purchasedProducts, string userId);
        
        Task<OperationResult<bool>> DeletePurchaseAsync(string id, string clientId);
    }
}
