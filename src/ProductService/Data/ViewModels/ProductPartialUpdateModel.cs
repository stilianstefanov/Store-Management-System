namespace ProductService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Product;

    public class ProductPartialUpdateModel
    {
        [Range(QuantityMinValue, QuantityMaxValue)]
        public int? Quantity { get; set; }

        public string? WarehouseId { get; set; }
    }
}
