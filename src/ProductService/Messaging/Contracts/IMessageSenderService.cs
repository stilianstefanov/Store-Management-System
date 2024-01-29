﻿namespace ProductService.Messaging.Contracts
{
    using Models;

    public interface IMessageSenderService
    {
        void PublishCreatedProduct(ProductCreatedDto productCreatedDto);

        void PublishUpdatedProduct(ProductUpdatedDto productUpdatedDto);
    }
}
