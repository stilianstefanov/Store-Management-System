namespace CreditService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.ViewModels.Borrower;
    using Data.Repositories.Contracts;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;

    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMapper _mapper;

        public BorrowerService(IBorrowerRepository borrowerRepository, IMapper mapper)
        {
            _borrowerRepository = borrowerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<BorrowerViewModel>>> GetAllBorrowersAsync(string userId)
        {
            var borrowers = await _borrowerRepository.GetAllBorrowersAsync(userId);

            return OperationResult<IEnumerable<BorrowerViewModel>>.Success(
                _mapper.Map<IEnumerable<BorrowerViewModel>>(borrowers)!);
        }

        public async Task<OperationResult<BorrowerViewModel>> GetBorrowerByIdAsync(string id, string userId)
        {
            var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);

            if (borrower == null || borrower.UserId != userId)
            {
                return OperationResult<BorrowerViewModel>.Failure(BorrowerNotFound, ErrorType.NotFound);
            }

            return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(borrower)!);
        }

        public async Task<OperationResult<BorrowerViewModel>> CreateBorrowerAsync(BorrowerCreateModel model, string userId)
        {
            var newBorrower = _mapper.Map<Borrower>(model)!;

            newBorrower.UserId = userId;

            await _borrowerRepository.AddBorrowerAsync(newBorrower);

            await _borrowerRepository.SaveChangesAsync();

            return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(newBorrower)!);
        }

        public async Task<OperationResult<BorrowerViewModel>> UpdateBorrowerAsync(string id, BorrowerUpdateModel model, string userId)
        {
            var borrowerToUpdate = await _borrowerRepository.GetBorrowerByIdAsync(id);

            if (borrowerToUpdate == null || borrowerToUpdate.UserId != userId)
            {
                return OperationResult<BorrowerViewModel>.Failure(BorrowerNotFound, ErrorType.NotFound);
            }

            _mapper.Map(model, borrowerToUpdate);

            await _borrowerRepository.SaveChangesAsync();

            return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(borrowerToUpdate)!);
        }

        public async Task<OperationResult<bool>> DeleteBorrowerAsync(string id, string userId)
        {
            var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);

            if (borrower == null || borrower.UserId != userId)
            {
                return OperationResult<bool>.Failure(BorrowerNotFound, ErrorType.NotFound);
            }

            await _borrowerRepository.DeleteBorrowerAsync(id);

            await _borrowerRepository.SaveChangesAsync();

            return OperationResult<bool>.Success(true);
        }

        public async Task<bool> BorrowerExistsAsync(string id)
        {
            return await _borrowerRepository.BorrowerExistsAsync(id);
        }

        public async Task<bool> IncreaseBorrowerCreditAsync(string id, decimal amount)
        {
            var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);

            if (borrower!.CreditLimit - borrower.CurrentCredit < amount)
            {
                return false;
            }

            borrower.CurrentCredit += amount;

            await _borrowerRepository.SaveChangesAsync();

            return true;
        }

        public async Task DecreaseBorrowerCreditAsync(string id, decimal amount)
        {
            var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);

            borrower!.CurrentCredit -= amount;

            if (borrower.CurrentCredit < 0)
            {
                borrower.CurrentCredit = 0;
            }

            await _borrowerRepository.SaveChangesAsync();
        }
    }
}
