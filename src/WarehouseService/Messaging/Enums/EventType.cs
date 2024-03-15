namespace WarehouseService.Messaging.Enums
{
    public enum EventType
    {
        ProductCreated = 0,
        ProductUpdated = 1,
        ProductDeleted = 2,
        ProductPartiallyUpdated = 3,
        MultipleProductsStockUpdated= 4,
        Undefined = 5
    }
}
