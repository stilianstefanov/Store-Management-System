namespace ProductService.Data.ViewModels
{
    public class ProductUpdateModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Barcode { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int MinQuantity { get; set; }

        public int MaxQuantity { get; set; }
    }
}
