namespace CreditService.Services.Contracts
{
    using Data.ViewModels;

    public interface IBorrowerService
    {
        Task<IEnumerable<BorrowerViewModel>> GetAllBorrowersAsync();

        Task<BorrowerViewModel> GetBorrowerByIdAsync(string id);
    }
}
