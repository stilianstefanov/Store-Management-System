namespace WarehouseService.Data.ViewModels
{
    public class WarehouseViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int ProductsCount { get; set; }
    }
}
