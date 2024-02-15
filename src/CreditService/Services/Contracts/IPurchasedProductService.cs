namespace CreditService.Services.Contracts
{
    using Data.ViewModels.PurchasedProduct;

    public interface IPurchasedProductService
    {
        Task <IEnumerable<PurchasedProductViewModel>> GetBoughtProductsByPurchaseIdAsync(string purchaseId);

        Task<decimal> DeleteBoughtProductByIdAsync(string id);

        Task DeleteBoughtProductsByPurchaseIdAsync(string purchaseId);

        Task<bool> ValidateProductsAsync(IEnumerable<PurchasedProductCreateModel> purchasedProducts);
    }
}
