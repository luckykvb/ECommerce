﻿namespace ECommerce.Api.Products.DB
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Inventory { get; set;}
    }
}
