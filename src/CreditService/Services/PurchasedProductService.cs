namespace CreditService.Services
{
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels.PurchasedProduct;

    public class PurchasedProductService : IPurchasedProductService
    {
        private readonly IPurchasedProductRepository _purchaseProductRepository;
        private readonly IProductGrpcClientService _productGrpcClient;

        public PurchasedProductService(
            IPurchasedProductRepository purchaseProductRepository, 
            IProductGrpcClientService productGrpcClient)
        {
            _purchaseProductRepository = purchaseProductRepository;
            _productGrpcClient = productGrpcClient;
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

        public async Task DeleteBoughtProductsByPurchaseIdAsync(string purchaseId)
        {
            var products = await _purchaseProductRepository.GetProductsByPurchaseIdAsync(purchaseId);

            foreach (var product in products)
            {
                product.IsDeleted = true;
            }

            await _purchaseProductRepository.SaveChangesAsync();
        }
    }
}
