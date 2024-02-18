namespace CreditService.Services.Contracts
{
    using Data.ViewModels.PurchasedProduct;
    using Utilities;

    public interface IPurchasedProductService
    {
        Task<OperationResult<IEnumerable<PurchasedProductViewModel>>> GetBoughtProductsByPurchaseIdAsync(string purchaseId);

        Task<OperationResult<bool>> DeleteBoughtProductByIdAsync(string borrowerId, string id);
    }
}
