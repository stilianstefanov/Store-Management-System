namespace WarehouseService.Data.ViewModels
{
    using Enums;
    using System.ComponentModel.DataAnnotations;
    using static Common.ApplicationConstants;
    using static Common.EntityValidationConstants.Product;

    public class ProductsAllQueryModel
    {
        public ProductsAllQueryModel()
        {
            CurrentPage = DefaultPage;
            ProductsPerPage = DefaultItemsPerPage;

            Products = new HashSet<ProductViewModel>();
        }

        public string? SearchTerm { get; set; }

        public bool BelowMinQty { get; set; }

        public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        [Range(PerPageMinValue, PerPageMaxValue)]
        public int ProductsPerPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
