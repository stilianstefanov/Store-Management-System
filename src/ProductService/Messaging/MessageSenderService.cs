namespace ProductService.Messaging
{
    using Contracts;
    using Models;
    using System.Text.Json;
    using static EventTypes;

    public class MessageSenderService : IMessageSenderService
    {
        private readonly IMessageBusClient _messageBusClient;

        public MessageSenderService(IMessageBusClient messageBusClient)
        {
            _messageBusClient = messageBusClient;
        }

        public void PublishCreatedProduct(ProductCreatedDto productCreatedDto)
        {
            productCreatedDto.Event = ProductCreated;

            var message = JsonSerializer.Serialize(productCreatedDto);

            _messageBusClient.SendMessage(message);
        }
    }
}
