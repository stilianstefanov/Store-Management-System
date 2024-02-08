namespace CreditService.Common
{
    public static class EntityValidationConstants
    {
        public static class Borrower    
        {
            public const int NameMaxLength = 50;

            public const int NameMinLength = 3;

            public const int SurnameMaxLength = 50;

            public const int SurnameMinLength = 3;

            public const int LastNameMaxLength = 50;

            public const int LastNameMinLength = 3;

            public const string CurrentCreditMinValue = "0.00";

            public const string CurrentCreditMaxValue = "99999";

            public const string CreditLimitMinValue = "0.00";

            public const string CreditLimitMaxValue = "99999";
        }

        public static class PurchaseProduct
        {
            public const int BoughtQuantityMinValue = 1;

            public const int BoughtQuantityMaxValue = 9999;

            public const string PurchasePriceMinValue = "0.01";

            public const string PurchasePriceMaxValue = "99999";
        }
    }
}
