namespace ProductService.Messaging.Contracts
{
    using Models;

    public interface IMessageBusClient
    {
        void SendMessage(string message);
    }
}
