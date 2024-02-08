namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Borrower;

    public interface IBorrowerService
    {
        Task<IEnumerable<BorrowerViewModel>> GetAllBorrowersAsync();

        Task<BorrowerViewModel> GetBorrowerByIdAsync(string id);

        Task<BorrowerViewModel> CreateBorrowerAsync(BorrowerCreateModel borrower);

        Task<BorrowerViewModel> UpdateBorrowerAsync(string id, BorrowerUpdateModel borrower);

        Task<bool> BorrowerExistsAsync(string id);
    }
}
