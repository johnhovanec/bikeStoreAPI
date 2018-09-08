using System;
using System.Collections.Generic;
using bikestoreAPI.Models;

namespace bikestoreAPI.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? CartTimeStamp { get; set; }
        public bool? OrderPlaced { get; set; }
        public DateTime? OrderPlacedTimeStamp { get; set; }
        //public string CartIdentifier { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }

        public Customer Customer { get; set; }
        //public ICollection<ShoppingCartProduct> ShoppingCartProduct { get; set; }
    }
}