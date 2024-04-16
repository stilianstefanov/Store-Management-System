namespace ProductService.Common
{
    public static class EntityValidationConstants
    {
        public static class Product
        {
            public const int NameMaxLength = 100;

            public const int NameMinLength = 3;

            public const int DescriptionMaxLength = 500;

            public const int BarcodeMaxLength = 50;

            public const int WarehouseIdMaxLength = 100;

            public const string PriceMinValue = "0.01";

            public const string PriceMaxValue = "999999999";

            public const int QuantityMinValue = 0;

            public const int QuantityMaxValue = 9999;

            public const int UserIdMaxLength = 50;

            public const int ProductSortingMinValue = 0;

            public const int ProductSortingMaxValue = 7;

            public const int PerPageMinValue = 10;

            public const int PerPageMaxValue = 20;
        }
    }
}
