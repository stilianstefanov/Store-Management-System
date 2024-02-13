namespace CreditService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.ViewModels.Borrower;
    using Data.Repositories.Contracts;

    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMapper _mapper;

        public BorrowerService(IBorrowerRepository borrowerRepository, IMapper mapper)
        {
            _borrowerRepository = borrowerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BorrowerViewModel>> GetAllBorrowersAsync()
        {
            var borrowers = await _borrowerRepository.GetAllBorrowersAsync();

            return _mapper.Map<IEnumerable<BorrowerViewModel>>(borrowers)!;
        }

        public async Task<BorrowerViewModel> GetBorrowerByIdAsync(string id)
        {
            var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);

            return _mapper.Map<BorrowerViewModel>(borrower)!;
        }

        public async Task<BorrowerViewModel> CreateBorrowerAsync(BorrowerCreateModel model)
        {
            var newBorrower = _mapper.Map<Borrower>(model)!;

            await _borrowerRepository.AddBorrowerAsync(newBorrower);

            await _borrowerRepository.SaveChangesAsync();

            return _mapper.Map<BorrowerViewModel>(newBorrower)!;
        }

        public async Task<BorrowerViewModel> UpdateBorrowerAsync(string id, BorrowerUpdateModel model)
        {
            var updatedBorrower = await _borrowerRepository.UpdateBorrowerAsync(id, _mapper.Map<Borrower>(model)!);

            await _borrowerRepository.SaveChangesAsync();

            return _mapper.Map<BorrowerViewModel>(updatedBorrower)!;
        }

        public async Task DeleteBorrowerAsync(string id)
        {
            await _borrowerRepository.DeleteBorrowerAsync(id);

            await _borrowerRepository.SaveChangesAsync();
        }

        public async Task<bool> BorrowerExistsAsync(string id)
        {
            return await _borrowerRepository.BorrowerExistsAsync(id);
        }

        public async Task<bool> IncreaseBorrowerCreditAsync(string id, decimal amount)
        {
            var borrower = await _borrowerRepository.GetBorrowerByIdAsync(id);

            if (borrower.CreditLimit - borrower.CurrentCredit < amount)
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

            borrower.CurrentCredit -= amount;

            if (borrower.CurrentCredit < 0)
            {
                borrower.CurrentCredit = 0;
            }

            await _borrowerRepository.SaveChangesAsync();
        }
    }
}
