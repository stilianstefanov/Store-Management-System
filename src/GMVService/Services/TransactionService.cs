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
            var baseQuery = _transactionRepository.GetAllAsync(userId)
                .Where(t => t.DateTime.Date == queryModel.Date.Date);

            var totalsTask = baseQuery
                .GroupBy(t => t.Type)
                .Select(g => new {
                    Type = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToArrayAsync();

            var transactions = await baseQuery
                .OrderByDescending(t => t.DateTime)
                .Skip((queryModel.CurrentPage - 1) * queryModel.ItemsPerPage)
                .Take(queryModel.ItemsPerPage)
                .ToArrayAsync();

            var totals = await totalsTask;

            queryModel.Transactions = _mapper.Map<ICollection<TransactionDetailsModel>>(transactions);
            queryModel.TotalPages = (int)Math.Ceiling((double)await baseQuery.CountAsync() / queryModel.ItemsPerPage);
            queryModel.TotalGmv = totals.Sum(t => t.TotalAmount);
            queryModel.TotalRegularGmv = totals.Where(t => t.Type == TransactionType.Regular).Sum(t => t.TotalAmount);
            queryModel.TotalDelayedGmv = totals.Where(t => t.Type == TransactionType.Delayed).Sum(t => t.TotalAmount);
        }
    }
}
