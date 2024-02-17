namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Borrower;
    using Utilities;

    public interface IBorrowerService
    {
        Task<OperationResult<IEnumerable<BorrowerViewModel>>> GetAllBorrowersAsync();

        Task<OperationResult<BorrowerViewModel>> GetBorrowerByIdAsync(string id);

        Task<OperationResult<BorrowerViewModel>> CreateBorrowerAsync(BorrowerCreateModel borrower);

        Task<OperationResult<BorrowerViewModel>> UpdateBorrowerAsync(string id, BorrowerUpdateModel borrower);

        Task<OperationResult<bool>> DeleteBorrowerAsync(string id);

        Task<bool> BorrowerExistsAsync(string id);

        Task<bool> IncreaseBorrowerCreditAsync(string id, decimal amount);

        Task DecreaseBorrowerCreditAsync(string id, decimal amount);
    }
}
