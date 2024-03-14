namespace DelayedPaymentService.Data.ViewModels.Client
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Client;

    public class ClientDecreaseCreditModel
    {
        [Range(typeof(decimal), CurrentCreditMinValue, CurrentCreditMaxValue)]
        public decimal Amount { get; set; }
    }
}
