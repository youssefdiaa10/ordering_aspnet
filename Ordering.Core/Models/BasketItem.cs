﻿namespace Ordering.Core.Models
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}