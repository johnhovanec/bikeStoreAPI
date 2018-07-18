using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikestoreAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? InventoryQuantity { get; set; }
        public string ImagePath { get; set; }

        //public ICollection<ShoppingCartProduct> ShoppingCartProduct { get; set; }
    }
}
