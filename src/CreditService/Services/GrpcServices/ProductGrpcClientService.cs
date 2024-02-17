namespace CreditService.Services.GrpcServices
{
    using AutoMapper;
    using Contracts;
    using CreditService.Services.GrpcServices.Contracts;
    using Data.ViewModels.PurchasedProduct;
    using Grpc.Core;
    using Grpc.Net.Client;
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

        public async Task<bool> ProductsExistAsync(IEnumerable<string> ids)
        {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcProductService"]!);

            var client = new ProductServiceGrpc.ProductServiceGrpcClient(channel);

            var request = new ProductsExistRequest
            {
                Ids = { ids }
            };

            try
            {
                var response = await client.ProductsExistAsync(request);

                return response.ProductsExist;
            }
            catch (RpcException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
