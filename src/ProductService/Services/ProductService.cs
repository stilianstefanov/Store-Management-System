namespace ProductService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models;
    using Data.ViewModels;
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

        public async Task<OperationResult<IEnumerable<ProductViewModel>>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();

                return OperationResult<IEnumerable<ProductViewModel>>
                    .Success(_mapper.Map<IEnumerable<ProductViewModel>>(products));
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<ProductViewModel>>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<ProductDetailsViewModel>> CreateAsync(ProductCreateModel model)
        {
            try
            {
                var warehouseExists = await ValidateWarehouseId(model.WarehouseId);

                if (!warehouseExists)
                {
                    return OperationResult<ProductDetailsViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
                }

                var newProduct = _mapper.Map<Product>(model);

                await _productRepository.AddAsync(newProduct);

                await _productRepository.SaveChangesAsync();

                _messageSender.PublishCreatedProduct(_mapper.Map<ProductCreatedDto>(newProduct));

                return OperationResult<ProductDetailsViewModel>.Success(
                    await MapProductDetailsModelWithWarehouse(newProduct));
            }
            catch (Exception ex)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<ProductDetailsViewModel>> GetByIdAsync(string id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                return product == null 
                    ? OperationResult<ProductDetailsViewModel>.Failure(ProductNotFound, ErrorType.NotFound)
                    : OperationResult<ProductDetailsViewModel>.Success(await MapProductDetailsModelWithWarehouse(product));
            }
            catch (Exception ex)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<ProductDetailsViewModel>> UpdateAsync(string id, ProductUpdateModel model)
        {
            try
            {
                var updatedProduct = await _productRepository.UpdateAsync(id, _mapper.Map<Product>(model));

                await _productRepository.SaveChangesAsync();

                _messageSender.PublishUpdatedProduct(_mapper.Map<ProductUpdatedDto>(updatedProduct));

                return OperationResult<ProductDetailsViewModel>.Success(
                    await MapProductDetailsModelWithWarehouse(updatedProduct));
            }
            catch (KeyNotFoundException)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ProductNotFound, ErrorType.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<ProductDetailsViewModel>> PartialUpdateAsync(string id, ProductPartialUpdateModel model)
        {
            try
            {
                var productToUpdate = await _productRepository.GetByIdAsync(id);

                if (productToUpdate == null)
                {
                    return OperationResult<ProductDetailsViewModel>.Failure(ProductNotFound, ErrorType.NotFound);
                }

                if (model.Quantity.HasValue)
                {
                    productToUpdate!.Quantity = model.Quantity.Value;
                }

                if (!string.IsNullOrEmpty(model.WarehouseId))
                {
                    var warehouseExists = await ValidateWarehouseId(model.WarehouseId);

                    if (!warehouseExists)
                    {
                        return OperationResult<ProductDetailsViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
                    }

                    productToUpdate!.WarehouseId = model.WarehouseId;
                }

                await _productRepository.SaveChangesAsync();

                _messageSender.PublishPartiallyUpdatedProduct(_mapper.Map<ProductPartialUpdatedDto>(productToUpdate));

                return OperationResult<ProductDetailsViewModel>.Success(await MapProductDetailsModelWithWarehouse(productToUpdate));
            }
            catch (Exception ex)
            {
                return OperationResult<ProductDetailsViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<bool>> DeleteAsync(string id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);

                await _productRepository.SaveChangesAsync();

                _messageSender.PublishDeletedProduct(new ProductDeletedDto { Id = id });

                return OperationResult<bool>.Success(true);
            }
            catch (KeyNotFoundException)
            {
                return OperationResult<bool>.Failure(ProductNotFound, ErrorType.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex.Message);
            }
        }

        private async Task<ProductDetailsViewModel> MapProductDetailsModelWithWarehouse(Product? product)
        {
            var productDetailsModel = _mapper.Map<ProductDetailsViewModel>(product);

            productDetailsModel.Warehouse = await _grpcClient.GetWarehouseById(product!.WarehouseId)!;

            return productDetailsModel;
        }

        private async Task<bool> ValidateWarehouseId(string warehouseId)
            => await _grpcClient.WarehouseExists(warehouseId);
    }
}
