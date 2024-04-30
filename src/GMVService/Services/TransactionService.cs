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

        public async Task<OperationResult<TransactionsQueryModel>> GetGmvDetailsAsync(string userId, TransactionsQueryModel queryModel)
        {
            switch (queryModel.Period)
            {
                case Period.Day: await GetTransactionsByDay(userId, queryModel); break;
                case Period.Month: await GetTransactionsByMonth(userId, queryModel); break;
                case Period.Year: await GetTransactionsByYear(userId, queryModel); break;
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
                .Select(g => new
                {
                    Type = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                });

            var transactions = await baseQuery
                .OrderByDescending(t => t.DateTime)
                .Skip((queryModel.CurrentPage - 1) * queryModel.ItemsPerPage)
                .Take(queryModel.ItemsPerPage)
                .ToArrayAsync();

            var totals = await totalsTask.ToArrayAsync();

            queryModel.Transactions = _mapper.Map<ICollection<TransactionDetailsModel>>(transactions);
            queryModel.TotalPages = (int)Math.Ceiling((double)await baseQuery.CountAsync() / queryModel.ItemsPerPage);
            queryModel.TotalGmv = totals.Sum(t => t.TotalAmount);
            queryModel.TotalRegularGmv = totals.Where(t => t.Type == TransactionType.Regular).Sum(t => t.TotalAmount);
            queryModel.TotalDelayedGmv = totals.Where(t => t.Type == TransactionType.Delayed).Sum(t => t.TotalAmount);
        }

        private async Task GetTransactionsByMonth(string userId, TransactionsQueryModel queryModel)
        {
            var startDate = new DateTime(queryModel.Date.Year, queryModel.Date.Month, 1);
            var endDate = startDate.AddMonths(1);

            var baseQuery = _transactionRepository.GetAllAsync(userId)
                .Where(t => t.DateTime >= startDate && t.DateTime < endDate);

            var aggregationQuery = baseQuery
                .GroupBy(t => t.DateTime.Date)
                .Select(g => new {
                    Date = g.Key,
                    TotalAmount = g.Sum(t => t.Amount),
                    TotalRegular = g.Where(t => t.Type == TransactionType.Regular).Sum(t => t.Amount),
                    TotalDelayed = g.Where(t => t.Type == TransactionType.Delayed).Sum(t => t.Amount)
                });

            var dailyTotals = await aggregationQuery
                .OrderByDescending(t => t.Date)
                .ToArrayAsync();

            queryModel.TotalGmv = dailyTotals.Sum(dt => dt.TotalAmount);
            queryModel.TotalRegularGmv = dailyTotals.Sum(dt => dt.TotalRegular);
            queryModel.TotalDelayedGmv = dailyTotals.Sum(dt => dt.TotalDelayed);

            queryModel.TransactionDailyTotals = dailyTotals
                .Skip((queryModel.CurrentPage - 1) * queryModel.ItemsPerPage)
                .Take(queryModel.ItemsPerPage)
                .Select(dt => new TransactionsDailyTotal
                {
                    Date = dt.Date,
                    TotalGmv = dt.TotalAmount,
                    TotalRegularGmv = dt.TotalRegular,
                    TotalDelayedGmv = dt.TotalDelayed
                }).ToArray();

            queryModel.TotalPages = (int)Math.Ceiling(dailyTotals.Count() / (double)queryModel.ItemsPerPage);
        }

        private async Task GetTransactionsByYear(string userId, TransactionsQueryModel queryModel)
        {
            var yearStart = new DateTime(queryModel.Date.Year, 1, 1);
            var yearEnd = yearStart.AddYears(1);

            var baseQuery = _transactionRepository.GetAllAsync(userId)
                .Where(t => t.DateTime >= yearStart && t.DateTime < yearEnd);

            var monthlyAggregations = baseQuery
                .GroupBy(t => t.DateTime.Month)
                .Select(g => new {
                    Month = g.Key,
                    TotalAmount = g.Sum(t => t.Amount),
                    TotalRegular = g.Where(t => t.Type == TransactionType.Regular).Sum(t => t.Amount),
                    TotalDelayed = g.Where(t => t.Type == TransactionType.Delayed).Sum(t => t.Amount)
                });

            var monthlyTotals = await monthlyAggregations
                .OrderBy(m => m.Month)
                .ToArrayAsync();

            queryModel.TotalGmv = monthlyTotals.Sum(mt => mt.TotalAmount);
            queryModel.TotalRegularGmv = monthlyTotals.Sum(mt => mt.TotalRegular);
            queryModel.TotalDelayedGmv = monthlyTotals.Sum(mt => mt.TotalDelayed);

            queryModel.TransactionMonthlyTotals = monthlyTotals.Select(mt => new TransactionsMonthlyTotal
            {
                Month = mt.Month,
                TotalGmv = mt.TotalAmount,
                TotalRegularGmv = mt.TotalRegular,
                TotalDelayedGmv = mt.TotalDelayed
            }).ToArray();

            queryModel.TotalPages = 1;
        }
    }
}
