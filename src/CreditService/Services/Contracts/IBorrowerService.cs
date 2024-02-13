namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Borrower;

    public interface IBorrowerService
    {
        Task<IEnumerable<BorrowerViewModel>> GetAllBorrowersAsync();

        Task<BorrowerViewModel> GetBorrowerByIdAsync(string id);

        Task<BorrowerViewModel> CreateBorrowerAsync(BorrowerCreateModel borrower);

        Task<BorrowerViewModel> UpdateBorrowerAsync(string id, BorrowerUpdateModel borrower);

        Task DeleteBorrowerAsync(string id);

        Task<bool> BorrowerExistsAsync(string id);

        Task<bool> IncreaseBorrowerCreditAsync(string id, decimal amount);

        Task DecreaseBorrowerCreditAsync(string id, decimal amount);
    }
}
