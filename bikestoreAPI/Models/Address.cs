using System.Collections.Generic;

namespace bikestoreAPI.Models
{
    public class Address
    {
        public enum AddressType { Billing, Shipping, BillingAndShipping }

        public int Id { get; set; }
        public AddressType Type { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public bool Default { get; set; }

        //
        public int? UserId { get; set; }
        public User User { get; set; }

        //
        public List<Order> Order { get; set; }
    }
}