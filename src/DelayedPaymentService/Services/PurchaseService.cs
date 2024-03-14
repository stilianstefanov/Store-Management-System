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
        private readonly IClientService _clientService;
        private readonly IProductGrpcClientService _productGrpcClient;

        public PurchaseService(
            IPurchaseRepository purchaseRepository, 
            IClientService clientService,
            IProductGrpcClientService productGrpcClient,
            IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _clientService = clientService;
            _productGrpcClient = productGrpcClient;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<PurchaseViewModel>>> GetPurchasesByClientIdAsync(string clientId)
        {
            if (!await ClientExistsAsync(clientId))
            {
                return OperationResult<IEnumerable<PurchaseViewModel>>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            var purchases = await _purchaseRepository.GetPurchasesByClientIdAsync(clientId);

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

        public async Task<OperationResult<PurchaseViewModel>> CreatePurchaseAsync(string clientId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var purchasedProductModels = purchasedProducts.ToArray();

            if (!await ClientExistsAsync(clientId))
            {
                return OperationResult<PurchaseViewModel>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            if (!await ValidateProductsAsync(purchasedProductModels))
            {
                return OperationResult<PurchaseViewModel>.Failure(ProductsNotFound, ErrorType.NotFound);
            }

            if (!await TryIncreaseClientCreditAsync(clientId, purchasedProductModels))
            {
                return OperationResult<PurchaseViewModel>.Failure(InsufficientCredit, ErrorType.BadRequest);
            }

            var newPurchase = await CreateNewPurchase(clientId, purchasedProductModels);

            return OperationResult<PurchaseViewModel>.Success(_mapper.Map<PurchaseViewModel>(newPurchase)!);
        }

        public async Task<OperationResult<bool>> DeletePurchaseAsync(string id, string clientId)
        {
            if (!await ClientExistsAsync(clientId))
            {
                return OperationResult<bool>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            var purchaseExists = await _purchaseRepository.PurchaseExistsAsync(id);

            if (!purchaseExists)
            {
                return OperationResult<bool>.Failure(PurchaseNotFound, ErrorType.NotFound);
            }
            
            var purchaseAmount = await _purchaseRepository.DeletePurchaseAsync(id);

            await _clientService.DecreaseClientCreditAsync(clientId, purchaseAmount);

            return OperationResult<bool>.Success(true);
        }

        private async Task<bool> ClientExistsAsync(string clientId)
            => await _clientService.ClientExistsAsync(clientId);

        private async Task<bool> ValidateProductsAsync(IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var productsExist =
                await _productGrpcClient.ProductsExistAsync(purchasedProducts.Select(p => p.ExternalId));

            return productsExist;
        }

        private async Task<bool> TryIncreaseClientCreditAsync(string clientId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var totalAmount = purchasedProducts.Sum(p => p.PurchasePrice * p.BoughtQuantity);

            return await _clientService.IncreaseClientCreditAsync(clientId, totalAmount);
        }

        private async Task<Purchase> CreateNewPurchase(string clientId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var newPurchase = new Purchase
            {
                ClientId = Guid.Parse(clientId),
                Date = DateTime.Now,
                Products = _mapper.Map<ICollection<PurchasedProduct>>(purchasedProducts)!
            };

            await _purchaseRepository.AddPurchaseAsync(newPurchase);

            await _purchaseRepository.SaveChangesAsync();

            return newPurchase;
        }
    }
}
