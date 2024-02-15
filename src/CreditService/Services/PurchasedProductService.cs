namespace CreditService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels.PurchasedProduct;

    public class PurchasedProductService : IPurchasedProductService
    {
        private readonly IPurchasedProductRepository _purchaseProductRepository;
        private readonly IProductGrpcClientService _productGrpcClient;
        private readonly IMapper _mapper;

        public PurchasedProductService(
            IPurchasedProductRepository purchaseProductRepository, 
            IProductGrpcClientService productGrpcClient,
            IMapper mapper)
        {
            _purchaseProductRepository = purchaseProductRepository;
            _productGrpcClient = productGrpcClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchasedProductViewModel>> GetBoughtProductsByPurchaseIdAsync(string purchaseId)
        {
            var purchasedProducts = await _purchaseProductRepository.GetProductsByPurchaseIdAsync(purchaseId);

            var productsDetails =
                await _productGrpcClient.GetProductsAsync(purchasedProducts.Select(p => p.ExternalId));

            var result = new List<PurchasedProductViewModel>();

            foreach (var product in purchasedProducts)
            {
                var productDetails = productsDetails.FirstOrDefault(p => p.ExternalId == product.ExternalId);

                var productViewModel = _mapper.Map<PurchasedProductViewModel>(product)!;

                productViewModel.ProductDetails = productDetails;

                result.Add(productViewModel);
            }

            return result;
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

        public async Task<bool> ValidateProductsAsync(IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var productsExist =
                await _productGrpcClient.ProductsExistAsync(purchasedProducts.Select(p => p.ExternalId));

            return productsExist;
        }
    }
}
