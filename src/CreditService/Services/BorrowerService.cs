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
            try
            {
                var borrowers = await _borrowerRepository.GetAllBorrowersAsync();

                return OperationResult<IEnumerable<BorrowerViewModel>>.Success(
                    _mapper.Map<IEnumerable<BorrowerViewModel>>(borrowers)!);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<BorrowerViewModel>>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<BorrowerViewModel>> GetBorrowerByIdAsync(string id)
        {
            try
            {
                var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);

                if (borrower == null)
                {
                    return OperationResult<BorrowerViewModel>.Failure(BorrowerNotFound, ErrorType.NotFound);
                }

                return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(borrower)!);
            }
            catch (Exception ex)
            {
                return OperationResult<BorrowerViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<BorrowerViewModel>> CreateBorrowerAsync(BorrowerCreateModel model)
        {
            try
            {
                var newBorrower = _mapper.Map<Borrower>(model)!;

                await _borrowerRepository.AddBorrowerAsync(newBorrower);

                await _borrowerRepository.SaveChangesAsync();

                return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(newBorrower)!);
            }
            catch (Exception ex)
            {
                return OperationResult<BorrowerViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<BorrowerViewModel>> UpdateBorrowerAsync(string id, BorrowerUpdateModel model)
        {
            try
            {
                var updatedBorrower = await _borrowerRepository.UpdateBorrowerAsync(id, _mapper.Map<Borrower>(model)!);

                await _borrowerRepository.SaveChangesAsync();

                return OperationResult<BorrowerViewModel>.Success(_mapper.Map<BorrowerViewModel>(updatedBorrower)!);
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<BorrowerViewModel>.Failure(ex.Message, ErrorType.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<BorrowerViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<bool>> DeleteBorrowerAsync(string id)
        {
            try
            {
                await _borrowerRepository.DeleteBorrowerAsync(id);

                await _borrowerRepository.SaveChangesAsync();

                return OperationResult<bool>.Success(true);
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<bool>.Failure(ex.Message, ErrorType.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex.Message);
            }
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
