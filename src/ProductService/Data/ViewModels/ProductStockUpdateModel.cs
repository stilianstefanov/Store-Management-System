﻿namespace ProductService.Data.ViewModels
{
    public class ProductStockUpdateModel
    {
        public string Id { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
