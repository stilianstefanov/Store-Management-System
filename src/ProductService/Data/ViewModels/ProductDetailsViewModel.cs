namespace ProductService.Data.ViewModels
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Barcode { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal Quantity { get; set; }

        public decimal MinQuantity { get; set; }

        public decimal MaxQuantity { get; set; }

        public WarehouseViewModel Warehouse { get; set; } = null!;
    }
}
