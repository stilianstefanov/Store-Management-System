namespace ProductService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models;
    using Data.ViewModels;
    using Messaging.Contracts;
    using Messaging.Models;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMessageSenderService _messageSender;


        public ProductService(IProductRepository productRepository, IMapper mapper, IMessageSenderService messageSender)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _messageSender = messageSender;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public async Task<ProductDetailsViewModel> CreateAsync(ProductCreateModel model)
        {
            var newProduct = _mapper.Map<Product>(model);

            await _productRepository.AddAsync(newProduct);

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishCreatedProduct(_mapper.Map<ProductCreatedDto>(newProduct));

            return _mapper.Map<ProductDetailsViewModel>(newProduct);
        }

        public async Task<ProductDetailsViewModel> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<ProductDetailsViewModel>(product);
        }

        public async Task<ProductDetailsViewModel> UpdateAsync(string id, ProductUpdateModel model)
        {
            var updatedProduct = await _productRepository.UpdateAsync(id, _mapper.Map<Product>(model));

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishUpdatedProduct(_mapper.Map<ProductUpdatedDto>(updatedProduct));

            return _mapper.Map<ProductDetailsViewModel>(updatedProduct);
        }

        public async Task DeleteAsync(string id)
        {
            await _productRepository.DeleteAsync(id);

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishDeletedProduct(new ProductDeletedDto { Id = id });
        }
    }
}
