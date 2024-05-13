namespace ProductService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Product;

    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(BarcodeMaxLength)]
        public string? Barcode { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public decimal DeliveryPrice { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal MinQuantity { get; set; }

        [Required]
        public decimal MaxQuantity { get; set; }

        [Required]
        [MaxLength(WarehouseIdMaxLength)]
        public string WarehouseId { get; set; } = null!;

        [Required]
        [MaxLength(UserIdMaxLength)]
        public string UserId { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
