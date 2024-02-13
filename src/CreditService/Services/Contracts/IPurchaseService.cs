﻿namespace CreditService.Services.Contracts
{
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchasedProduct;

    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseViewModel>> GetPurchasesByBorrowerIdAsync(string borrowerId);

        Task<PurchaseViewModel> GetPurchaseByIdAsync(string id);
        
        Task<PurchaseViewModel> CreatePurchaseAsync(string borrowerId, IEnumerable<PurchasedProductCreateModel> purchasedProducts);
        
        Task DeletePurchaseAsync(string id);

        Task CompletePurchaseAsync();
    }
}
