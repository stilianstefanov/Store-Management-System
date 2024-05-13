namespace ProductService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Product;

    public class ProductPartialUpdateModel
    {
        [Range(typeof(decimal),QuantityMinValue, QuantityMaxValue)]
        public decimal? Quantity { get; set; }

        public string? WarehouseId { get; set; }
    }
}
