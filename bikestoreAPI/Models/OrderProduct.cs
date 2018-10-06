﻿namespace bikestoreAPI.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal? UnitPrice { get; set; }

        //
        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}