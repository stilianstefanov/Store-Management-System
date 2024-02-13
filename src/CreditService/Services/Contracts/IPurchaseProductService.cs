namespace CreditService.Services.Contracts
{
    using Data.ViewModels.PurchasedProduct;

    public interface IPurchaseProductService
    {
        Task <IEnumerable<PurchasedProductViewModel>> GetBoughtProductsByPurchaseIdAsync(string purchaseId);

        Task<decimal> DeleteBoughtProductByIdAsync(string id);
    }
}
