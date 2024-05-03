namespace GMVService.Messaging.Models
{
    public class TransactionCreateDto
    {
        public decimal TotalAmount { get; set; }

        public string TransactionType { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
