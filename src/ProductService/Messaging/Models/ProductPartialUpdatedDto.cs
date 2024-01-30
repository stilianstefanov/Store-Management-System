namespace ProductService.Messaging.Models
{
    public class ProductPartialUpdatedDto
    {
        public string Id { get; set; } = null!;

        public int? Quantity { get; set; }

        public string? WarehouseId { get; set; }
    }
}
