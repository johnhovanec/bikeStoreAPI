﻿using System;
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
        public string PaymentMethod { get; set; }

        public Customer Customer { get; set; }
        //public ICollection<ShoppingCartProduct> ShoppingCartProduct { get; set; }
    }
}