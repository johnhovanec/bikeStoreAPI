using System;
using System.Collections.Generic;

namespace bikestoreAPI.Models
{
    public class User
    {
        public enum UserType { Customer, Reports, Admin };

        public int Id { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLogin { get; set; }
        public string PwdResetToken { get; set; }
        public DateTime? PwdTokenTimeStamp { get; set; }
        public int? FailedLogins { get; set; }
        public UserType Type { get; set; }
        public DateTime? PwdTimeStamp { get; set; }
        public string Password { get; set; }

        //public ShoppingCart ShoppingCart { get; set; }
        public List<ShoppingCart> ShoppingCart { get; set; }
        public List<PaymentMethod> PaymentMethod { get; set; }
        public List<Address> Address { get; set; }
        public List<Order> Order { get; set; }
    }
}