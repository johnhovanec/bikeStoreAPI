using System;
using System.Collections.Generic;

namespace bikestoreAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public decimal Tax { get; set; }
        public string SourceCode { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        // 
        public int UserId { get; set; }
        public User User { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethod { get; set; }

        //
        public List<OrderProduct> OrderProduct { get; set; }
    }
}