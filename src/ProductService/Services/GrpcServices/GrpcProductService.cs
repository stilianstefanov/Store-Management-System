namespace ProductService.Services.GrpcServices
{
    using Grpc.Core;
    using AutoMapper;
    using Data.ViewModels;
    using Google.Protobuf.WellKnownTypes;
    using Services.Contracts;
    using Utilities.Enums;

    public class GrpcProductService : ProductServiceGrpc.ProductServiceGrpcBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public GrpcProductService(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public override async Task<GetProductsByMultipleIdsResponse> GetProductsByMultipleIds(
            GetProductByMultipleIdsRequest request, ServerCallContext context)
        {
            try
            {
                var products = await _productService.GetByIdsAsync(request.Ids);

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

        public override async Task<Empty> DecreaseProductsStocks(DecreaseProductsStocksRequest request, ServerCallContext context)
        {
            try
            {
                var productsToUpdate = _mapper.Map<IEnumerable<ProductStockUpdateModel>>(request.Products);

                var result = await _productService.DecreaseStocksAsync(productsToUpdate, request.UserId);

                if (!result.IsSuccess)
                {
                    switch (result.ErrorType)
                    {
                        case ErrorType.NotFound:
                            throw new RpcException(new Status(StatusCode.NotFound, result.ErrorMessage!));
                        case ErrorType.BadRequest:
                            throw new RpcException(new Status(StatusCode.InvalidArgument, result.ErrorMessage!));
                        default:
                            throw new RpcException(new Status(StatusCode.Internal, result.ErrorMessage!));
                    }
                }

                return new Empty();
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
