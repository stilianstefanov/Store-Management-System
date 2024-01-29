namespace ProductService.Messaging.Models
{
    public class ProductDeletedDto
    {
        public string Id { get; set; } = null!;

        public string Event { get; set; } = null!;
    }
}
