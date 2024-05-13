namespace WarehouseService.Messaging.Models
{
    public class ProductUpdatedDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal MinQuantity { get; set; }

        public decimal MaxQuantity { get; set; }

        public string Event { get; set; } = null!;
    }
}
