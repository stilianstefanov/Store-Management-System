namespace ProductService.Services.GrpcServices.Contracts
{
    using Data.ViewModels;

    public interface IWarehouseGrpcClientService
    {
        Task<WarehouseViewModel> GetWarehouseById(string id);

        Task<bool> WarehouseExists(string id);
    }
}
