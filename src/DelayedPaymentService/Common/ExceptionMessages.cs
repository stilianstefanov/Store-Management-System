namespace DelayedPaymentService.Common
{
    public static class ExceptionMessages
    {
        public const string ClientNotFound = "Client with the provided ID does not exist";

        public const string PurchaseNotFound = "Purchase with the provided ID does not exist";

        public const string ProductNotFound = "Product with the provided ID does not exist";

        public const string InsufficientCredit = "Insufficient credit";

        public const string ProductsNotFound = "Product or Products with the provided IDs do not exist";

        public const string GeneralErrorMessage = "An error occurred while processing your request. Please try again later.";
    }
}
