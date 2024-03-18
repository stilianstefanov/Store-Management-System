namespace DelayedPaymentService.Services.GrpcServices
{
    using AutoMapper;
    using Grpc.Core;
    using Grpc.Net.Client;
    using Contracts;
    using Data.ViewModels.PurchasedProduct;
    using ProductService;

    public class ProductGrpcClientService : IProductGrpcClientService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductGrpcClientService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDetailsViewModel>> GetProductsAsync(IEnumerable<string> ids)
        {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcProductService"]!);

            var client = new ProductServiceGrpc.ProductServiceGrpcClient(channel);

            var request = new GetProductByMultipleIdsRequest()
            {
                Ids = { ids }
            };

            try
            {
                var response = await client.GetProductsByMultipleIdsAsync(request);

                return response.Products.Select(p => _mapper.Map<ProductDetailsViewModel>(p))!;
            }
            catch (RpcException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DecreaseProductsStocksAsync(IEnumerable<PurchasedProductCreateModel> purchasedProducts, string userId)
        {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcProductService"]!);

            var client = new ProductServiceGrpc.ProductServiceGrpcClient(channel);

            var request = new DecreaseProductsStocksRequest()
            {
                Products = { purchasedProducts.Select(p => _mapper.Map<GrpcProductStockDecreaseModel>(p)) },
                UserId = userId
            };

            try
            { 
                await client.DecreaseProductsStocksAsync(request);
            }
            catch (RpcException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
