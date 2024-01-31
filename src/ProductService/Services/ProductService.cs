namespace ProductService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models;
    using Data.ViewModels;
    using Messaging.Contracts;
    using Messaging.Models;
    using static Common.ExceptionMessages;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMessageSenderService _messageSender;
        private readonly IWarehouseGrpcClientService _grpcClient;


        public ProductService(IProductRepository productRepository,
            IMapper mapper, 
            IMessageSenderService messageSender,
            IWarehouseGrpcClientService grpcClient)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _messageSender = messageSender;
            _grpcClient = grpcClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public async Task<ProductDetailsViewModel> CreateAsync(ProductCreateModel model)
        {
            ValidateWarehouseId(model.WarehouseId);

            var newProduct = _mapper.Map<Product>(model);

            await _productRepository.AddAsync(newProduct);

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishCreatedProduct(_mapper.Map<ProductCreatedDto>(newProduct));

            return GetProductDetailsViewModel(newProduct);
        }

        public async Task<ProductDetailsViewModel> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return GetProductDetailsViewModel(product);
        }

        public async Task<ProductDetailsViewModel> UpdateAsync(string id, ProductUpdateModel model)
        {
            var updatedProduct = await _productRepository.UpdateAsync(id, _mapper.Map<Product>(model));

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishUpdatedProduct(_mapper.Map<ProductUpdatedDto>(updatedProduct));

           return GetProductDetailsViewModel(updatedProduct);
        }

        public async Task<ProductDetailsViewModel> PartialUpdateAsync(string id, ProductPartialUpdateModel model)
        {
            var productToUpdate = await _productRepository.GetByIdAsync(id);

            if (model.Quantity.HasValue)
            {
                productToUpdate!.Quantity = model.Quantity.Value;
            }

            if (!string.IsNullOrEmpty(model.WarehouseId))
            {
                ValidateWarehouseId(model.WarehouseId);

                productToUpdate!.WarehouseId = model.WarehouseId;
            }

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishPartiallyUpdatedProduct(_mapper.Map<ProductPartialUpdatedDto>(productToUpdate));

            return GetProductDetailsViewModel(productToUpdate);
        }

        public async Task DeleteAsync(string id)
        {
            await _productRepository.DeleteAsync(id);

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishDeletedProduct(new ProductDeletedDto { Id = id });
        }

        private ProductDetailsViewModel GetProductDetailsViewModel(Product? product)
        {
            var productDetailsModel = _mapper.Map<ProductDetailsViewModel>(product);

            productDetailsModel.Warehouse = _grpcClient.GetWarehouseById(product!.WarehouseId)!;

            return productDetailsModel;
        }

        private bool ValidateWarehouseId(string warehouseId)
        {
            var warehouseExists = _grpcClient.WarehouseExists(warehouseId);

            if (!warehouseExists)
            {
                throw new InvalidOperationException(WarehouseNotFound);
            }

            return true;
        }
    }
}
