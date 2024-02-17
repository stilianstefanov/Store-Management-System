namespace WarehouseService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWarehouseService _warehouseService;

        public ProductService(IProductRepository productRepository, IWarehouseService warehouseService, IMapper mapper)
        {
            _productRepository = productRepository;
            _warehouseService = warehouseService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<ProductViewModel>>> GetProductsByWarehouseIdAsync(string warehouseId)
        {
            try
            {
                var warehouseExists = await _warehouseService.ExistsAsync(warehouseId);

                if (!warehouseExists)
                {
                    return OperationResult<IEnumerable<ProductViewModel>>.Failure(WarehouseNotFound, ErrorType.NotFound);
                }

                var products = await _productRepository.GetProductsByWarehouseIdAsync(warehouseId);

                return OperationResult<IEnumerable<ProductViewModel>>
                    .Success(_mapper.Map<IEnumerable<ProductViewModel>>(products));
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<ProductViewModel>>.Failure(ex.Message);
            }
        }
    }
}
