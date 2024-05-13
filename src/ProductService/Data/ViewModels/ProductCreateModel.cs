namespace ProductService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Product;

    public class ProductCreateModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [MaxLength(BarcodeMaxLength)]
        public string? Barcode { get; set; }

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal DeliveryPrice { get; set; }

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Range(typeof(decimal),QuantityMinValue, QuantityMaxValue)]
        public decimal Quantity { get; set; }

        [Range(typeof(decimal),QuantityMinValue, QuantityMaxValue)]
        public decimal MinQuantity { get; set; }

        [Range(typeof(decimal),QuantityMinValue, QuantityMaxValue)]
        public decimal MaxQuantity { get; set; }

        public string WarehouseId { get; set; } = null!;
    }
}
