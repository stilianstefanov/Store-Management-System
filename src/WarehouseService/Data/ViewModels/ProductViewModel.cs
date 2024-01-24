namespace WarehouseService.Data.ViewModels
{
    public class ProductViewModel
    {
        public string ExternalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public int MinQuantity { get; set; }

        public int MaxQuantity { get; set; }
    }
}
