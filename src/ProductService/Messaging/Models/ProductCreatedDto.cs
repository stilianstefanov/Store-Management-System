namespace ProductService.Messaging.Models
{
    public class ProductCreatedDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public int MinQuantity { get; set; }

        public int MaxQuantity { get; set; }

        public string WarehouseId { get; set; } = null!;

        public string Event { get; set; } = null!;
    }
}
