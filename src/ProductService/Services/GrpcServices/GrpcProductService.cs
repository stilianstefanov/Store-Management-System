namespace ProductService.Services.GrpcServices
{
    using AutoMapper;
    using Data.Contracts;
    using Grpc.Core;

    public class GrpcProductService : ProductServiceGrpc.ProductServiceGrpcBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GrpcProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public override async Task<GetProductsByMultipleIdsResponse> GetProductsByMultipleIds(
            GetProductByMultipleIdsRequest request, ServerCallContext context)
        {
            try
            {
                var products = await _productRepository.GetByIdsAsync(request.Ids);

                var response = new GetProductsByMultipleIdsResponse();

                foreach (var product in products)
                {
                    response.Products.Add(_mapper.Map<GrpcProductModel>(product));
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<ProductsExistResponse> ProductsExist(ProductsExistRequest request, ServerCallContext context)
        {
            try
            {
                var productsExist = await _productRepository.ProductsExistAsync(request.Ids);

                return new ProductsExistResponse { ProductsExist = productsExist };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
