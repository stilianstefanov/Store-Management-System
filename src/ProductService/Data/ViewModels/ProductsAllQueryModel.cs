namespace ProductService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.EntityValidationConstants.Product;
    using static Common.ApplicationConstants;

    public class ProductsAllQueryModel
    {
        public ProductsAllQueryModel()
        {
            CurrentPage = DefaultPage;
            ProductsPerPage = DefaultItemsPerPage;

            Products = new HashSet<ProductViewModel>();
        }

        public string? SearchTerm { get; set; }

        [Range(ProductSortingMinValue, ProductSortingMaxValue)]
        public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        [Range(PerPageMinValue, PerPageMaxValue)]
        public int ProductsPerPage { get; set; }

        public int TotalPages { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }
    }
}
