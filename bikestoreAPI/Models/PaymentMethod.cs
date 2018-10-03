using System;
using System.Collections.Generic;

namespace bikestoreAPI.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public bool Default { get; set; }
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string NameOnCard { get; set; }
        public string CVVNumber { get; set; }
        public DateTime? TimeStamp { get; set; }

        //
        public int UserId { get; set; }
        public User User { get; set; }

        //
        public List<Order> Order { get; set; }
    }

}