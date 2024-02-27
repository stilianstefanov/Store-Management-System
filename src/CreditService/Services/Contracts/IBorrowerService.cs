namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Borrower;
    using Utilities;

    public interface IBorrowerService
    {
        Task<OperationResult<IEnumerable<BorrowerViewModel>>> GetAllBorrowersAsync(string userId);

        Task<OperationResult<BorrowerViewModel>> GetBorrowerByIdAsync(string id, string userId);

        Task<OperationResult<BorrowerViewModel>> CreateBorrowerAsync(BorrowerCreateModel borrower, string userId);

        Task<OperationResult<BorrowerViewModel>> UpdateBorrowerAsync(string id, BorrowerUpdateModel borrower, string userId);

        Task<OperationResult<bool>> DeleteBorrowerAsync(string id, string userId);

        Task<bool> BorrowerExistsAsync(string id);

        Task<bool> IncreaseBorrowerCreditAsync(string id, decimal amount);

        Task DecreaseBorrowerCreditAsync(string id, decimal amount);
    }
}
