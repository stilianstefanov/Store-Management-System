namespace CreditService.Services
{
    using Contracts;
    using Data.Repositories.Contracts;
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchaseProduct;

    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<PurchaseViewModel>> GetPurchasesByBorrowerIdAsync(string borrowerId)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseViewModel> CreatePurchaseAsync(string borrowerId, IEnumerable<PurchaseProductCreateModel> purchaseProducts)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePurchaseAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
