namespace ProductService.Messaging.Models
{
    using Enums;

    public class MultipleProductsStockUpdateDto
    {
        public string Event { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public TransactionType TransactionType { get; set; }

        public IEnumerable<ProductPartialUpdatedDto> Products { get; set; } = null!;
    }
}
