namespace WarehouseService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Warehouse;

    public class WarehouseCreateModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(TypeMaxLength, MinimumLength = TypeMinLength)]
        public string Type { get; set; } = null!;
    }
}
