namespace ProductService.Services.Contracts
{
    using Data.ViewModels;

    public interface IWarehouseGrpcClientService
    {
        WarehouseViewModel? GetWarehouseById(string id);
    }
}
