namespace ProductService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models;
    using Data.ViewModels;
    using Data.ViewModels.Enums;
    using GrpcServices.Contracts;
    using Messaging.Contracts;
    using Messaging.Models;
    using Messaging.Models.Enums;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<OperationResult<ProductsAllQueryModel>> GetAllAsync(string userId, ProductsAllQueryModel queryModel)
        {
            var productsQuery = _productRepository.GetAllAsync(userId);

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                var wildCardSearchTerm = $"{queryModel.SearchTerm}%";

                productsQuery = productsQuery
                    .Where(p => EF.Functions.Like(p.Name, wildCardSearchTerm) ||
                                EF.Functions.Like(p.Description, wildCardSearchTerm) ||
                                EF.Functions.Like(p.Barcode, wildCardSearchTerm));
            }

            productsQuery = queryModel.Sorting switch
            {
                ProductSorting.NameAscending => productsQuery.OrderBy(p => p.Name),
                ProductSorting.NameDescending => productsQuery.OrderByDescending(p => p.Name),
                ProductSorting.PriceAscending => productsQuery.OrderBy(p => p.Price),
                ProductSorting.PriceDescending => productsQuery.OrderByDescending(p => p.Price),
                ProductSorting.QuantityAscending => productsQuery.OrderBy(p => p.Quantity),
                ProductSorting.QuantityDescending => productsQuery.OrderByDescending(p => p.Quantity),
                ProductSorting.DeliveryPriceAscending => productsQuery.OrderBy(p => p.DeliveryPrice),
                ProductSorting.DeliveryPriceDescending => productsQuery.OrderByDescending(p => p.DeliveryPrice),
                _ => productsQuery.OrderBy(p => p.Name)
            };

            var products = await productsQuery                
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .ToArrayAsync();

            var totalPages = (int)Math.Ceiling(await productsQuery.CountAsync() / (double)queryModel.ProductsPerPage);

            queryModel.TotalPages = totalPages;
            queryModel.Products = _mapper.Map<ICollection<ProductViewModel>>(products);

            return OperationResult<ProductsAllQueryModel>.Success(queryModel);
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

        public async Task<OperationResult<ProductDashViewModel>> GetByBarcodeAsync(string barcode, string userId)
        {
            var product = await _productRepository.GetByBarcodeAsync(barcode);

            if (product == null || product.UserId != userId)
            {
                return OperationResult<ProductDashViewModel>.Failure(ProductNotFound, ErrorType.NotFound);
            }

            return OperationResult<ProductDashViewModel>.Success(_mapper.Map<ProductDashViewModel>(product));
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

        public async Task<OperationResult<bool>> DecreaseStocksAsync(
            IEnumerable<ProductStockUpdateModel> models, string userId, TransactionType transactionType = TransactionType.Regular)
        {
            var updatedProducts = new List<Product>();

            var productStockUpdateModels = models.ToArray();
            foreach (var model in productStockUpdateModels)
            {
                var productToUpdate = await _productRepository.GetByIdAsync(model.Id);

                if (productToUpdate == null || productToUpdate.UserId != userId)
                {
                    return OperationResult<bool>.Failure(ProductNotFound, ErrorType.NotFound);
                }

                productToUpdate.Quantity -= model.Quantity;

                updatedProducts.Add(productToUpdate);
            }

            await _productRepository.SaveChangesAsync();

            _messageSender.PublishMultipleProductsStockUpdate(new MultipleProductsStockUpdateDto
            {
                TotalAmount = productStockUpdateModels.Sum(s => s.Price * s.Quantity),
                TransactionType = transactionType.ToString(),
                UserId = userId,
                Products = _mapper.Map<IEnumerable<ProductPartialUpdatedDto>>(updatedProducts)
            });

            return OperationResult<bool>.Success(true);
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

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids)
            => await _productRepository.GetByIdsAsync(ids);

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
