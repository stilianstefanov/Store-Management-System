namespace CreditService.Services
{
    using Contracts;
    using Data.ViewModels.PurchaseProduct;

    public class PurchaseProductService : IPurchaseProductService
    {
        public async Task<IEnumerable<PurchaseProductViewModel>> GetBoughtProductsByPurchaseIdAsync(string purchaseId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteBoughtProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
