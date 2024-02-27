namespace ProductService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models;
    using Data.ViewModels;
    using GrpcServices.Contracts;
    using Messaging.Contracts;
    using Messaging.Models;
    using Utilities;
    using Utilities.Enums;
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

        public async Task<OperationResult<IEnumerable<ProductViewModel>>> GetAllAsync(string userId)
        {
            var products = await _productRepository.GetAllAsync(userId);

            return OperationResult<IEnumerable<ProductViewModel>>
                .Success(_mapper.Map<IEnumerable<ProductViewModel>>(products));
        }

        public async Task<OperationResult<ProductDetailsViewModel>> CreateAsync(ProductCreateModel model, string userId)
        {
            var warehouseExists = await ValidateWarehouseId(model.WarehouseId);

            if (!warehouseExists)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
            }

            var newProduct = _mapper.Map<Product>(model);

            newProduct.UserId = userId;

            await _productRepository.AddAsync(newProduct);

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishCreatedProduct(_mapper.Map<ProductCreatedDto>(newProduct));

            return OperationResult<ProductDetailsViewModel>.Success(
                await MapProductDetailsModelWithWarehouse(newProduct));
        }

        public async Task<OperationResult<ProductDetailsViewModel>> GetByIdAsync(string id, string userId)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null || product.UserId != userId)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ProductNotFound, ErrorType.NotFound);
            }

            return OperationResult<ProductDetailsViewModel>.Success(await MapProductDetailsModelWithWarehouse(product));
        }

        public async Task<OperationResult<ProductDetailsViewModel>> UpdateAsync(string id, ProductUpdateModel model, string userId)
        {
            var productToUpdate = await _productRepository.GetByIdAsync(id);

            if (productToUpdate == null || productToUpdate.UserId != userId)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ProductNotFound, ErrorType.NotFound);
            }

            _mapper.Map(model, productToUpdate);

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishUpdatedProduct(_mapper.Map<ProductUpdatedDto>(productToUpdate));

            return OperationResult<ProductDetailsViewModel>.Success(await MapProductDetailsModelWithWarehouse(productToUpdate));
        }

        public async Task<OperationResult<ProductDetailsViewModel>> PartialUpdateAsync(string id, ProductPartialUpdateModel model, string userId)
        {
            var productToUpdate = await _productRepository.GetByIdAsync(id);
            
            if (productToUpdate == null || productToUpdate.UserId != userId)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ProductNotFound, ErrorType.NotFound);
            }

            if (model.Quantity.HasValue)
            {
                productToUpdate.Quantity = model.Quantity.Value;
            }

            if (!string.IsNullOrEmpty(model.WarehouseId))
            {
                var warehouseExists = await ValidateWarehouseId(model.WarehouseId);

                if (!warehouseExists)
                {
                    return OperationResult<ProductDetailsViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
                }

                productToUpdate.WarehouseId = model.WarehouseId;
            }

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishPartiallyUpdatedProduct(_mapper.Map<ProductPartialUpdatedDto>(productToUpdate));

            return OperationResult<ProductDetailsViewModel>.Success(await MapProductDetailsModelWithWarehouse(productToUpdate));
        }

        public async Task<OperationResult<bool>> DeleteAsync(string id, string userId)
        {
            var productToDelete = await _productRepository.GetByIdAsync(id);

            if (productToDelete == null || productToDelete.UserId != userId)
            {
                return OperationResult<bool>.Failure(ProductNotFound, ErrorType.NotFound);
            }
            
            await _productRepository.DeleteAsync(id);

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishDeletedProduct(new ProductDeletedDto { Id = id });

            return OperationResult<bool>.Success(true);
        }

        private async Task<ProductDetailsViewModel> MapProductDetailsModelWithWarehouse(Product? product)
        {
            var productDetailsModel = _mapper.Map<ProductDetailsViewModel>(product);

            productDetailsModel.Warehouse = await _grpcClient.GetWarehouseById(product!.WarehouseId);

            return productDetailsModel;
        }

        private async Task<bool> ValidateWarehouseId(string warehouseId)
            => await _grpcClient.WarehouseExists(warehouseId);
    }
}
