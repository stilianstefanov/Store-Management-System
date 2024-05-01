namespace GMVService.Messaging.Contracts
{
    public interface IEventProcessor
    {
        Task ProcessEventAsync(string message);
    }
}
