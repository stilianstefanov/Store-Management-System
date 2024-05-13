namespace ProductService.Data.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
