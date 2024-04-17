namespace WarehouseService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels;
    using Data.ViewModels.Enums;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<OperationResult<ProductsAllQueryModel>> GetProductsByWarehouseIdAsync(
            string warehouseId, ProductsAllQueryModel queryModel)
        {
            var warehouseExists = await _warehouseService.ExistsAsync(warehouseId);

            if (!warehouseExists)
            {
                return OperationResult<ProductsAllQueryModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
            }

            var productsQuery = _productRepository.GetProductsByWarehouseIdAsync(warehouseId);

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                var wildCardSearchTerm = $"%{queryModel.SearchTerm}%";

                productsQuery = productsQuery
                    .Where(p => EF.Functions.Like(p.Name, wildCardSearchTerm));
            }

            if (queryModel.BelowMinQty)
            {
                productsQuery = productsQuery
                    .Where(p => p.Quantity < p.MinQuantity);
            }

            productsQuery = queryModel.Sorting switch
            {
                ProductSorting.NameAscending => productsQuery.OrderBy(p => p.Name),
                ProductSorting.NameDescending => productsQuery.OrderByDescending(p => p.Name),
                ProductSorting.QuantityAscending => productsQuery.OrderBy(p => p.Quantity),
                ProductSorting.QuantityDescending => productsQuery.OrderByDescending(p => p.Quantity),
                ProductSorting.MaxQuantityAscending => productsQuery.OrderBy(p => p.MaxQuantity),
                ProductSorting.MaxQuantityDescending => productsQuery.OrderByDescending(p => p.MaxQuantity),
                ProductSorting.MinQuantityAscending => productsQuery.OrderBy(p => p.MinQuantity),
                ProductSorting.MinQuantityDescending => productsQuery.OrderByDescending(p => p.MinQuantity),
                ProductSorting.SuggestedOrderQtyAscending => productsQuery.OrderBy(p => p.MaxQuantity - p.Quantity),
                ProductSorting.SuggestedOrderQtyDescending => productsQuery.OrderByDescending(p => p.MaxQuantity - p.Quantity),
                _ => productsQuery.OrderBy(p => p.Name)
            };

            var products = await productsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .ToArrayAsync();

            var totalPages = (int)Math.Ceiling(await productsQuery.CountAsync() / (double)queryModel.ProductsPerPage);

            queryModel.TotalPages = totalPages;
            queryModel.Products = _mapper.Map<IEnumerable<ProductViewModel>>(products);

            return OperationResult<ProductsAllQueryModel>.Success(queryModel);
        }
    }
}
