namespace WarehouseService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByWarehouseIdAsync(string warehouseId)
        {
            var products = await _productRepository.GetProductsByWarehouseIdAsync(warehouseId);

            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }
    }
}
