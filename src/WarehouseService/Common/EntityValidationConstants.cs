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

            public const int UserIdMaxLength = 40;

            public const int WarehouseSortingMinValue = 0;

            public const int WarehouseSortingMaxValue = 3;

            public const int PerPageMinValue = 10;

            public const int PerPageMaxValue = 20;
        }

        public static class Product
        {
            public const int ExternalIdMaxLength = 100;

            public const int ProductSortingMinValue = 0;

            public const int ProductSortingMaxValue = 9;

            public const int PerPageMinValue = 10;

            public const int PerPageMaxValue = 20;
        }
    }
}
