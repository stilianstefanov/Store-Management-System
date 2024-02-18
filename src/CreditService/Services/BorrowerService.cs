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

        public async Task<OperationResult<IEnumerable<BorrowerViewModel>>> GetAllBorrowersAsync()
        {
            var borrowers = await _borrowerRepository.GetAllBorrowersAsync();

            return OperationResult<IEnumerable<BorrowerViewModel>>.Success(
                _mapper.Map<IEnumerable<BorrowerViewModel>>(borrowers)!);
        }

        public async Task<OperationResult<BorrowerViewModel>> GetBorrowerByIdAsync(string id)
        {
            var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);
            
            return borrower == null 
                ? OperationResult<BorrowerViewModel>.Failure(BorrowerNotFound, ErrorType.NotFound)
                : OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(borrower)!);
        }

        public async Task<OperationResult<BorrowerViewModel>> CreateBorrowerAsync(BorrowerCreateModel model)
        {
            var newBorrower = _mapper.Map<Borrower>(model)!;

            await _borrowerRepository.AddBorrowerAsync(newBorrower);

            await _borrowerRepository.SaveChangesAsync();

            return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(newBorrower)!);
        }

        public async Task<OperationResult<BorrowerViewModel>> UpdateBorrowerAsync(string id, BorrowerUpdateModel model)
        {
            var updatedBorrower = await _borrowerRepository.UpdateBorrowerAsync(id, _mapper.Map<Borrower>(model)!);

            if (updatedBorrower == null)
            {
                return OperationResult<BorrowerViewModel>.Failure(BorrowerNotFound, ErrorType.NotFound);
            }

            await _borrowerRepository.SaveChangesAsync();

            return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(updatedBorrower)!);
        }

        public async Task<OperationResult<bool>> DeleteBorrowerAsync(string id)
        {
            var borrowerExists = await _borrowerRepository.BorrowerExistsAsync(id);

            if (!borrowerExists)
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
