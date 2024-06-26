﻿namespace ProductService.Messaging.Models
{
    public class ProductPartialUpdatedDto
    {
        public string Id { get; set; } = null!;

        public decimal? Quantity { get; set; }

        public string? WarehouseId { get; set; }

        public string Event { get; set; } = null!;
    }
}
