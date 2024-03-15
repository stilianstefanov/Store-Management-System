namespace ProductService.Common
{
    public static class ExceptionMessages
    {
        public const string ProductNotFound = "Product with the provided ID does not exist.";

        public const string WarehouseNotFound = "Warehouse with the provided ID does not exist.";

        public const string GeneralErrorMessage = "An error occurred while processing your request. Please try again later.";

        public const string InsufficientStock = "Insufficient stock for the product with name: {0}. Barcode: {1}";
    }
}
