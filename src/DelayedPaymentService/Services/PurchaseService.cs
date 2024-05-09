namespace DelayedPaymentService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repositories.Contracts;
    using Data.ViewModels.Purchase;
    using Data.ViewModels.Purchase.Enums;
    using Data.ViewModels.PurchasedProduct;
    using GrpcServices.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;
    using static Common.ApplicationConstants;

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

        public async Task<OperationResult<PurchasesAllQueryModel>> GetPurchasesByClientIdAsync(string clientId, PurchasesAllQueryModel queryModel)
        {
            if (!await ClientExistsAsync(clientId))
            {
                return OperationResult<PurchasesAllQueryModel>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            var purchasesQuery = _purchaseRepository.GetPurchasesByClientIdAsync(clientId);

            if (queryModel.Date != default)
            {
                purchasesQuery = purchasesQuery.Where(p => p.Date.Date == queryModel.Date.Date);
            }

            purchasesQuery = queryModel.Sorting switch
            {
                PurchaseSorting.DateLatest => purchasesQuery.OrderByDescending(p => p.Date),
                PurchaseSorting.DateOldest => purchasesQuery.OrderBy(p => p.Date),
                PurchaseSorting.AmountDescending => purchasesQuery.OrderByDescending(p => p.Products.Sum(pp => pp.PurchasePrice * pp.BoughtQuantity)),
                PurchaseSorting.AmountAscending => purchasesQuery.OrderBy(p => p.Products.Sum(pp => pp.PurchasePrice * pp.BoughtQuantity)),
                _ => purchasesQuery.OrderByDescending(p => p.Date)
            };

            var purchases = await purchasesQuery
                .Skip((queryModel.CurrentPage - 1) * DefaultItemsPerPage)
                .Take(DefaultItemsPerPage)
                .ToArrayAsync();

            var totalPages = (int)Math.Ceiling(await purchasesQuery.CountAsync() / (double)DefaultItemsPerPage);

            queryModel.TotalPages = totalPages;
            queryModel.Purchases = _mapper.Map<IEnumerable<PurchaseViewModel>>(purchases)!;

            return OperationResult<PurchasesAllQueryModel>.Success(queryModel);
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

        public async Task<OperationResult<PurchaseViewModel>> CreatePurchaseAsync(
            string clientId, IEnumerable<PurchasedProductCreateModel> purchasedProducts, string userId)
        {
            var purchasedProductModels = purchasedProducts.ToArray();

            if (!await ClientExistsAsync(clientId))
            {
                return OperationResult<PurchaseViewModel>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            if (!await ClientHasEnoughCreditAsync(clientId, purchasedProductModels))
            {
                return OperationResult<PurchaseViewModel>.Failure(InsufficientCredit, ErrorType.BadRequest);
            }

            var productsStocksUpdated = await UpdateProductsStocksAsync(purchasedProductModels, userId);

            if (!productsStocksUpdated.IsSuccess)
            {
                return OperationResult<PurchaseViewModel>.Failure(productsStocksUpdated.ErrorMessage!, productsStocksUpdated.ErrorType);
            }

            await IncreaseClientCreditAsync(clientId, purchasedProductModels);

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

        private async Task IncreaseClientCreditAsync(string clientId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var totalAmount = purchasedProducts.Sum(p => p.Price * p.Quantity);
            
            await _clientService.IncreaseClientCreditAsync(clientId, totalAmount);
        }

        private async Task<bool> ClientHasEnoughCreditAsync(string clientId,
            IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var totalAmount = purchasedProducts.Sum(p => p.Price * p.Quantity);

            return await _clientService.ClientHasEnoughCreditAsync(clientId, totalAmount);
        }

        private async Task<Purchase> CreateNewPurchase(string clientId, IEnumerable<PurchasedProductCreateModel> purchasedProducts)
        {
            var newPurchase = new Purchase
            {
                ClientId = Guid.Parse(clientId),
                Date = GetCurrentDateTime(),
                Products = _mapper.Map<ICollection<PurchasedProduct>>(purchasedProducts)!
            };

            await _purchaseRepository.AddPurchaseAsync(newPurchase);

            await _purchaseRepository.SaveChangesAsync();

            return newPurchase;
        }

        private async Task<OperationResult<bool>> UpdateProductsStocksAsync(IEnumerable<PurchasedProductCreateModel> purchasedProducts, string userId)
        {
            try
            {
                await _productGrpcClient.DecreaseProductsStocksAsync(purchasedProducts, userId);

                return OperationResult<bool>.Success(true);
            }
            catch (KeyNotFoundException e)
            {
                return OperationResult<bool>.Failure(e.Message, ErrorType.NotFound);
            }
            catch (ArgumentException e)
            {
                return OperationResult<bool>.Failure(e.Message, ErrorType.BadRequest);
            }
        }

        private DateTime GetCurrentDateTime()
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            var userDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);

            return userDateTime;
        }
    }
}
