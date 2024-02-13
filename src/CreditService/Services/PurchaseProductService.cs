namespace CreditService.Services
{
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels.PurchasedProduct;

    public class PurchaseProductService : IPurchaseProductService
    {
        private readonly IPurchasedProductRepository _purchaseProductRepository;

        public PurchaseProductService(IPurchasedProductRepository purchaseProductRepository)
        {
            _purchaseProductRepository = purchaseProductRepository;
        }

        public async Task<IEnumerable<PurchasedProductViewModel>> GetBoughtProductsByPurchaseIdAsync(string purchaseId)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> DeleteBoughtProductByIdAsync(string id)
        {
            var amount = await _purchaseProductRepository.DeleteProductByIdAsync(id);

            await _purchaseProductRepository.SaveChangesAsync();

            return amount;
        }
    }
}
