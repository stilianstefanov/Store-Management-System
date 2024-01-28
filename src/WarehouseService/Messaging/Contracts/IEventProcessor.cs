namespace WarehouseService.Messaging.Contracts
{
    public interface IEventProcessor
    {
        Task ProcessEventAsync(string message);
    }
}
