namespace GMVService.Messaging
{
    using System.Text.Json;
    using AutoMapper;
    using Contracts;
    using Enums;
    using Models;

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEventType(message);

            switch (eventType)
            {
                case EventType.MultipleProductsStockUpdated:
                    await ProcessMultipleProductsStockUpdatedEventAsync(message);
                    break;
            }
        }

        private async Task ProcessMultipleProductsStockUpdatedEventAsync(string message)
        {

        }

        private EventType DetermineEventType(string notificationMessage)
        {
            Console.WriteLine($"Determining event type for message: {notificationMessage}");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            if (Enum.TryParse(eventType!.Event, out EventType result))
            {
                return result;
            }

            return EventType.Undefined;
        }
    }
}
