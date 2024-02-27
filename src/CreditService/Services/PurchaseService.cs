namespace CreditService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repositories.Contracts;
    using Data.ViewModels.Purchase;
    using Data.ViewModels.PurchasedProduct;
    using GrpcServices.Contracts;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;

    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;
        private readonly IBorrowerService _borrowerService;
        private readonly IProductGrpcClientService _productGrpcClient;

        public PurchaseService(
            IPurchaseRepository purchaseRepository, 
            IBorrowerService borrowerService,
            IProductGrpcClientService productGrpcClient,
            IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _borrowerService = borrowerService;
            _productGrpcClient = productGrpcClient;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<PurchaseViewModel>>> GetPurchasesByBorrowerIdAsync(string borrowerId)
        {
            if (!await BorrowerExistsAsync(borrowerId))
            {
                return OperationResult<IEnumerable<PurchaseViewModel>>.Failure(BorrowerNotFound, ErrorType.NotFound);
            }

            var purchases = await _purchaseRepository.GetPurchasesByBorrowerIdAsync(borrowerId);

            return OperationResult<IEnumerable<PurchaseViewModel>>.Success(
                _mapper.Map<IEnumerable<PurchaseViewModel>>(purchases)!);
        }

        public async Task<OperationResult<PurchaseViewModel>> GetPurchaseByIdAsync(string id)
        {
            var purchase = await _purchaseRepository.GetPurchaseByIdAsync(id);

            if (purchase == null)
            {
                return OperationResult<PurchaseViewModel>.Failure(PurchaseNotFound, ErrorType.NotFound);
            }

            return OperationResult<PurchaseViewModel>.Success(_mapper.Map<PurchaseViewModel>(purchase)!);
        }

        public async Task<OperationResult<PurchaseViewModel>> CreatePurchaseAsync(string borrowerId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var purchasedProductModels = purchasedProducts.ToArray();

            if (!await BorrowerExistsAsync(borrowerId))
            {
                return OperationResult<PurchaseViewModel>.Failure(BorrowerNotFound, ErrorType.NotFound);
            }

            if (!await ValidateProductsAsync(purchasedProductModels))
            {
                return OperationResult<PurchaseViewModel>.Failure(ProductsNotFound, ErrorType.NotFound);
            }

            if (!await TryIncreaseBorrowerCreditAsync(borrowerId, purchasedProductModels))
            {
                return OperationResult<PurchaseViewModel>.Failure(InsufficientCredit, ErrorType.BadRequest);
            }

            var newPurchase = await CreateNewPurchase(borrowerId, purchasedProductModels);

            return OperationResult<PurchaseViewModel>.Success(_mapper.Map<PurchaseViewModel>(newPurchase)!);
        }

        public async Task<OperationResult<bool>> DeletePurchaseAsync(string id, string borrowerId)
        {
            if (!await BorrowerExistsAsync(borrowerId))
            {
                return OperationResult<bool>.Failure(BorrowerNotFound, ErrorType.NotFound);
            }

            var purchaseExists = await _purchaseRepository.PurchaseExistsAsync(id);

            if (!purchaseExists)
            {
                return OperationResult<bool>.Failure(PurchaseNotFound, ErrorType.NotFound);
            }
            
            var purchaseAmount = await _purchaseRepository.DeletePurchaseAsync(id);

            await _borrowerService.DecreaseBorrowerCreditAsync(borrowerId, purchaseAmount);

            return OperationResult<bool>.Success(true);
        }

        private async Task<bool> BorrowerExistsAsync(string borrowerId)
            => await _borrowerService.BorrowerExistsAsync(borrowerId);

        private async Task<bool> ValidateProductsAsync(IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var productsExist =
                await _productGrpcClient.ProductsExistAsync(purchasedProducts.Select(p => p.ExternalId));

            return productsExist;
        }

        private async Task<bool> TryIncreaseBorrowerCreditAsync(string borrowerId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var totalAmount = purchasedProducts.Sum(p => p.PurchasePrice * p.BoughtQuantity);

            return await _borrowerService.IncreaseBorrowerCreditAsync(borrowerId, totalAmount);
        }

        private async Task<Purchase> CreateNewPurchase(string borrowerId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var newPurchase = new Purchase
            {
                BorrowerId = Guid.Parse(borrowerId),
                Date = DateTime.UtcNow,
                Products = _mapper.Map<ICollection<PurchasedProduct>>(purchasedProducts)!
            };

            await _purchaseRepository.AddPurchaseAsync(newPurchase);

            await _purchaseRepository.SaveChangesAsync();

            return newPurchase;
        }
    }
}
