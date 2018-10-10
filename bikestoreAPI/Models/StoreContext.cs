using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikestoreAPI.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProduct { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<ShippingMethod> ShippingMethod { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<BackOrder> BackOrder { get; set; }
        public DbSet<Session> Session { get; set; }
    }
}
