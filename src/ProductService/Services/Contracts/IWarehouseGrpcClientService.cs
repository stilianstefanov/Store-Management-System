namespace ProductService.Services.Contracts
{
    using Data.ViewModels;

    public interface IWarehouseGrpcClientService
    {
        Task<WarehouseViewModel> GetWarehouseById(string id);

        Task<bool> WarehouseExists(string id);
    }
}
