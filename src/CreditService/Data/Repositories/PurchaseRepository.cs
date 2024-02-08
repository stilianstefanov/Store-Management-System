﻿namespace CreditService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly CreditDbContext _dbContext;

        public PurchaseRepository(CreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByBorrowerIdAsync(string borrowerId)
        {
            var purchases = await _dbContext.Purchases
                .Where(p => p.BorrowerId.ToString() == borrowerId && !p.IsDeleted)
                .ToArrayAsync();

            return purchases;
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            await _dbContext.Purchases.AddAsync(purchase);
        }

        public async Task DeletePurchaseAsync(string id)
        {
            var purchase = await _dbContext.Purchases
                .FirstOrDefaultAsync(p => p.Id.ToString() == id);

            if (purchase == null)
            {
                throw new InvalidOperationException(PurchaseNotFound);
            }

            purchase.IsDeleted = true;
        }
    }
}
