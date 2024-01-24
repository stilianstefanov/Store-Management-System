namespace WarehouseService.Common
{
    public static class EntityValidationConstants
    {
        public static class Warehouse
        {
            public const int NameMaxLength = 100;

            public const int NameMinLength = 3;

            public const int TypeMaxLength = 100;

            public const int TypeMinLength = 3;
        }

        public static class Product
        {
            public const int ExternalIdMaxLength = 100;
        }
    }
}
