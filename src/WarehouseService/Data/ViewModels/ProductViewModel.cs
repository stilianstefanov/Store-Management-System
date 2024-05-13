namespace WarehouseService.Data.ViewModels
{
    public class ProductViewModel
    {
        public string ExternalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal MinQuantity { get; set; }

        public decimal MaxQuantity { get; set; }

        public int SuggestedOrderQty { get; set; }
    }
}
