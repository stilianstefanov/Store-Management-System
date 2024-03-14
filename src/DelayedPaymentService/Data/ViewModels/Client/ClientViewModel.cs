namespace DelayedPaymentService.Data.ViewModels.Client
{
    public class ClientViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Surname { get; set; }

        public string LastName { get; set; } = null!;

        public decimal CurrentCredit { get; set; }

        public decimal CreditLimit { get; set; }
    }
}
