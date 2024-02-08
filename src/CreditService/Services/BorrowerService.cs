﻿namespace CreditService.Services
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
    }
}