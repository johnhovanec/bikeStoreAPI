using System;

namespace bikestoreAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? FailedLogins { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}