namespace ProductService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Product;

    public class ProductPartialUpdateModel
    {
        [Range(QuantityMinValue, QuantityMaxValue)]
        public int? Quantity { get; set; }

        //ToDO Add validation for WarehouseId via gRPC
        public string? WarehouseId { get; set; }
    }
}
