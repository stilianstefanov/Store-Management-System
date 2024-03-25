namespace DelayedPaymentService.Data.ViewModels.Client
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Client;

    public class ClientPartialUpdateModel
    {
        [Range(typeof(decimal), CurrentCreditMinValue, CurrentCreditMaxValue)]
        public decimal? CurrentCredit { get; set; }

        [Range(typeof(decimal), CreditLimitMinValue, CreditLimitMaxValue)]
        public decimal? CreditLimit { get; set; }
    }
}
