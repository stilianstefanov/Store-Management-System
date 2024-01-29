namespace WarehouseService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Warehouse;

    public class Warehouse
    {
        public Warehouse()
        {
            Id = Guid.NewGuid();
            Products = new HashSet<Product>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TypeMaxLength)]
        public string Type { get; set; } = null!;


        public ICollection<Product> Products { get; set; }
    }
}
