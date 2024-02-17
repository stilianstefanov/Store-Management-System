namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchasedProduct;
    using Utilities;

    public interface IPurchaseService
    {
        Task<OperationResult<IEnumerable<PurchaseViewModel>>> GetPurchasesByBorrowerIdAsync(string borrowerId);

        Task<OperationResult<PurchaseViewModel>> GetPurchaseByIdAsync(string id);
        
        Task<OperationResult<PurchaseViewModel>> CreatePurchaseAsync(string borrowerId, IEnumerable<PurchasedProductCreateModel> purchasedProducts);
        
        Task<OperationResult<bool>> DeletePurchaseAsync(string id, string borrowerId);

        Task<bool> PurchaseExistsAsync(string id);
    }
}
