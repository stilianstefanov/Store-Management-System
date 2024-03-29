﻿namespace ProductService.Messaging
{
    using System.Text.Json;
    using Contracts;
    using Models;
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

        public void PublishUpdatedProduct(ProductUpdatedDto productUpdatedDto)
        {
            productUpdatedDto.Event = ProductUpdated;

            var message = JsonSerializer.Serialize(productUpdatedDto);

            _messageBusClient.SendMessage(message);
        }

        public void PublishPartiallyUpdatedProduct(ProductPartialUpdatedDto productPartialUpdatedDto)
        {
            productPartialUpdatedDto.Event = ProductPartiallyUpdated;

            var message = JsonSerializer.Serialize(productPartialUpdatedDto);

            _messageBusClient.SendMessage(message);
        }

        public void PublishDeletedProduct(ProductDeletedDto productDeletedDto)
        {
            productDeletedDto.Event = ProductDeleted;

            var message = JsonSerializer.Serialize(productDeletedDto);

            _messageBusClient.SendMessage(message);
        }

        public void PublishMultipleProductsStockUpdate(MultipleProductsStockUpdateDto productStockUpdatedDto)
        {
            productStockUpdatedDto.Event = MultipleProductsStockUpdated;

            var message = JsonSerializer.Serialize(productStockUpdatedDto);

            _messageBusClient.SendMessage(message);
        }
    }
}
