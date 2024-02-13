namespace CreditService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repositories.Contracts;
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchasedProduct;

    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseViewModel>> GetPurchasesByBorrowerIdAsync(string borrowerId)
        {
            var purchases = await _purchaseRepository.GetPurchasesByBorrowerIdAsync(borrowerId);

            return _mapper.Map<IEnumerable<PurchaseViewModel>>(purchases)!;
        }

        public async Task<PurchaseViewModel> GetPurchaseByIdAsync(string id)
        {
            var purchase = await _purchaseRepository.GetPurchaseByIdAsync(id);

            return _mapper.Map<PurchaseViewModel>(purchase)!;
        }

        public async Task<PurchaseViewModel> CreatePurchaseAsync(string borrowerId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var newPurchase = new Purchase
            {
                BorrowerId = Guid.Parse(borrowerId),
                Date = DateTime.UtcNow,
                Products = _mapper.Map<ICollection<PurchasedProduct>>(purchasedProducts)!
            };

            await _purchaseRepository.AddPurchaseAsync(newPurchase);

            return _mapper.Map<PurchaseViewModel>(newPurchase)!;
        }

        public async Task<decimal> DeletePurchaseAsync(string id)
        {
            var amount = await _purchaseRepository.DeletePurchaseAsync(id);

            await _purchaseRepository.SaveChangesAsync();

            return amount;
        }

        public async Task DeletePurchasesByBorrowerIdAsync(string borrowerId)
        {
            var purchases = await _purchaseRepository.GetPurchasesByBorrowerIdAsync(borrowerId);

            foreach (var purchase in purchases)
            {
                purchase.IsDeleted = true;

                foreach (var product in purchase.Products)
                {
                    product.IsDeleted = true;
                }
            }

            await _purchaseRepository.SaveChangesAsync();
        }

        public async Task CompletePurchaseAsync()
        {
            await _purchaseRepository.SaveChangesAsync();
        }

        public async Task<bool> PurchaseExistsAsync(string id)
        {
            return await _purchaseRepository.PurchaseExistsAsync(id);
        }
    }
}
