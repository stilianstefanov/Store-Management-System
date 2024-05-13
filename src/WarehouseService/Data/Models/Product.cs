namespace WarehouseService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Product;

    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ExternalIdMaxLength)]
        public string ExternalId { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal MinQuantity { get; set; }

        [Required]
        public decimal MaxQuantity { get; set; }

        public bool IsDeleted { get; set; }


        [ForeignKey(nameof(Warehouse))]
        public Guid WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; } = null!;
    }
}
