﻿namespace DelayedPaymentService.Data.Repositories.Contracts
{
    using Models;

    public interface IPurchasedProductRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<PurchasedProduct>> GetProductsByPurchaseIdAsync(string purchaseId);

        Task<decimal> DeleteProductByIdAsync(string id);

        Task<bool> ProductExistsAsync(string id);
    }
}
