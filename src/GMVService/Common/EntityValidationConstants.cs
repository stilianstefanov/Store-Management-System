namespace GMVService.Common
{
    public static class EntityValidationConstants
    {
        public const string AmountMinValue = "0.01";

        public const string AmountMaxValue = "999999999";

        public const int UserIdMaxLength = 50;

        public const int PerPageMinValue = 10;

        public const int PerPageMaxValue = 20;

        public const int TransactionTypeMinValue = 0;

        public const int TransactionTypeMaxValue = 1;

        public const int PeriodMinValue = 0;

        public const int PeriodMaxValue = 2;
    }
}
