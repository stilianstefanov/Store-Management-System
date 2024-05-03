namespace ProductService.Messaging.Models
{

    public class MultipleProductsStockUpdateDto
    {
        public string Event { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public string TransactionType { get; set; } = null!;

        public IEnumerable<ProductPartialUpdatedDto> Products { get; set; } = null!;
    }
}
