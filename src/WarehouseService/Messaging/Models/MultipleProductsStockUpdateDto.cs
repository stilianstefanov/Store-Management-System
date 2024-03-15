namespace WarehouseService.Messaging.Models
{
    public class MultipleProductsStockUpdateDto
    {
        public string Event { get; set; } = null!;

        public IEnumerable<ProductPartialUpdatedDto> Products { get; set; } = null!;
    }
}
