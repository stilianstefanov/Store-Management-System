namespace WarehouseService.Messaging
{
    using System.Text.Json;
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repositories.Contracts;
    using Enums;
    using Models;

    public class EventProcessor :IEventProcessor
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
                case EventType.ProductCreated:
                    await ProcessProductCreatedEventAsync(message);
                    break;
                case EventType.ProductUpdated:
                    await ProcessProductUpdatedEventAsync(message);
                    break;
                case EventType.ProductDeleted:
                    await ProcessProductDeletedEventAsync(message);
                    break;
                case EventType.ProductPartiallyUpdated:
                    await ProcessProductPartiallyUpdatedEventAsync(message);
                    break;
            }
        }

        private async Task ProcessProductCreatedEventAsync(string message)
        {
            using var scope = _scopeFactory.CreateScope();

            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();


            var productCreatedDto = JsonSerializer.Deserialize<ProductCreatedDto>(message);

            var product = _mapper.Map<Product>(productCreatedDto);

            var productExists = await productRepository.ExternalProductExistsAsync(product.ExternalId);

            if (productExists) return;
            
            await productRepository.AddProductAsync(product);

            await productRepository.SaveChangesAsync();
        }

        private async Task ProcessProductUpdatedEventAsync(string message)
        {
            using var scope = _scopeFactory.CreateScope();

            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();


            var productUpdatedDto = JsonSerializer.Deserialize<ProductUpdatedDto>(message);

            var productExists = await productRepository.ExternalProductExistsAsync(productUpdatedDto!.Id);

            if (!productExists) return;

            await productRepository.UpdateProductAsync(productUpdatedDto!);

            await productRepository.SaveChangesAsync();
        }

        private async Task ProcessProductPartiallyUpdatedEventAsync(string message)
        {
            using var scope = _scopeFactory.CreateScope();

            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();


            var productPartialUpdatedDto = JsonSerializer.Deserialize<ProductPartialUpdatedDto>(message);

            var productToUpdate = await productRepository.GetProductByExternalId(productPartialUpdatedDto!.Id);

            if (productToUpdate == null) return;

            if (productPartialUpdatedDto.Quantity.HasValue)
            {
                productToUpdate.Quantity = productPartialUpdatedDto.Quantity.Value;
            }

            if (!string.IsNullOrEmpty(productPartialUpdatedDto.WarehouseId))
            {
                productToUpdate.WarehouseId = Guid.Parse(productPartialUpdatedDto.WarehouseId);
            }

            await productRepository.SaveChangesAsync();
        }

        private async Task ProcessProductDeletedEventAsync(string message)
        {
            using var scope = _scopeFactory.CreateScope();

            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();


            var productDeletedDto = JsonSerializer.Deserialize<ProductDeletedDto>(message);

            var productExists = await productRepository.ExternalProductExistsAsync(productDeletedDto!.Id);

            if (!productExists) return;

            await productRepository.DeleteProductAsync(productDeletedDto!.Id);

            await productRepository.SaveChangesAsync();
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
