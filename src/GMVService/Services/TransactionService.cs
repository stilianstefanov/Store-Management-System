namespace GMVService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models.Enums;
    using Data.ViewModels;
    using Data.ViewModels.Enums;
    using Microsoft.EntityFrameworkCore;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<TransactionsQueryModel>> GetGmvAsync(string userId, TransactionsQueryModel queryModel)
        {
            switch (queryModel.Period)
            {
                case Period.Day: await GetTransactionsByDay(userId, queryModel);
                    break;
                default: return OperationResult<TransactionsQueryModel>.Failure(InvalidPeriod, ErrorType.BadRequest);
            }

            return OperationResult<TransactionsQueryModel>.Success(queryModel);
        }

        private async Task GetTransactionsByDay(string userId, TransactionsQueryModel queryModel)
        {
            var transactionsQuery = _transactionRepository.GetAllAsync(userId);
            transactionsQuery = transactionsQuery
                .Where(t => t.DateTime.Date == queryModel.Date.Date);

            queryModel.TotalGmv = await transactionsQuery.SumAsync(t => t.Amount);

            queryModel.TotalRegularGmv = await transactionsQuery
                .Where(t => t.Type == TransactionType.Regular)
                .SumAsync(t => t.Amount);

            queryModel.TotalDelayedGmv = await transactionsQuery
                .Where(t => t.Type == TransactionType.Delayed)
                .SumAsync(t => t.Amount);

            var transactions = await transactionsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ItemsPerPage)
                .Take(queryModel.ItemsPerPage)
                .OrderByDescending(t => t.DateTime)
                .ToArrayAsync();

            var totalPages = (int)Math.Ceiling(await transactionsQuery.CountAsync() / (double)queryModel.ItemsPerPage);

            queryModel.TotalPages = totalPages;
            queryModel.Transactions = _mapper.Map<ICollection<TransactionDetailsModel>>(transactions);
        }
    }
}
