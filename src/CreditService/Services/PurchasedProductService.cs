namespace CreditService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using GrpcServices.Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels.PurchasedProduct;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;

    public class PurchasedProductService : IPurchasedProductService
    {
        private readonly IPurchasedProductRepository _purchaseProductRepository;
        private readonly IProductGrpcClientService _productGrpcClient;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
    

        public PurchasedProductService(
            IPurchasedProductRepository purchaseProductRepository,
            IPurchaseRepository purchaseRepository,
            IProductGrpcClientService productGrpcClient,
            IClientService clientService,
            IMapper mapper)
        {
            _purchaseProductRepository = purchaseProductRepository;
            _productGrpcClient = productGrpcClient;
            _purchaseRepository = purchaseRepository;
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<PurchasedProductViewModel>>> GetBoughtProductsByPurchaseIdAsync(string purchaseId)
        {
            var purchaseExists = await _purchaseRepository.PurchaseExistsAsync(purchaseId);

            if (!purchaseExists)
            {
                return OperationResult<IEnumerable<PurchasedProductViewModel>>.Failure(PurchaseNotFound, ErrorType.NotFound);
            }

            var purchasedProducts = await _purchaseProductRepository.GetProductsByPurchaseIdAsync(purchaseId);

            var purchasedProductsArr =  purchasedProducts.ToArray();

            if (!purchasedProductsArr.Any())
            {
                return OperationResult<IEnumerable<PurchasedProductViewModel>>.Success(
                    Enumerable.Empty<PurchasedProductViewModel>());
            }

            var resultProducts = await MapProductDetailsAsync(purchasedProductsArr);

            return OperationResult<IEnumerable<PurchasedProductViewModel>>.Success(resultProducts);
        }

        public async Task<OperationResult<bool>> DeleteBoughtProductByIdAsync(string clientId, string id)
        {
            var clientExists = await _clientService.ClientExistsAsync(clientId);

            if (!clientExists) return OperationResult<bool>.Failure(ClientNotFound);

            var productExists = await _purchaseProductRepository.ProductExistsAsync(id);

            if (!productExists) return OperationResult<bool>.Failure(ProductNotFound);
            
            var amount = await _purchaseProductRepository.DeleteProductByIdAsync(id);

            await _purchaseProductRepository.SaveChangesAsync();

            await _clientService.DecreaseClientCreditAsync(clientId, amount);

            return OperationResult<bool>.Success(true);
        }

        private async Task<IEnumerable<PurchasedProductViewModel>> MapProductDetailsAsync(
            IEnumerable<PurchasedProduct> purchasedProducts)
        {
            var purchasedProductsArr = purchasedProducts.ToArray();

            var productsDetails = 
                await _productGrpcClient.GetProductsAsync(purchasedProductsArr.Select(p => p.ExternalId));

            var productDetailsLookup = productsDetails.ToDictionary(p => p.ExternalId);

            var result = purchasedProductsArr.Select(product =>
            {
                var viewModel = _mapper.Map<PurchasedProductViewModel>(product)!;

                productDetailsLookup.TryGetValue(product.ExternalId, out var productDetails);

                viewModel.ProductDetails = productDetails!;

                return viewModel;

            }).ToArray();

            return result;
        }
    }
}
