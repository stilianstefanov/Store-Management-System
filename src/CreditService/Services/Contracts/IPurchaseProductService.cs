namespace CreditService.Services.Contracts
{
    using Data.ViewModels.PurchaseProduct;

    public interface IPurchaseProductService
    {
        Task <IEnumerable<PurchaseProductViewModel>> GetBoughtProductsByPurchaseIdAsync(string purchaseId);

        Task DeleteBoughtProductByIdAsync(string id);
    }
}
