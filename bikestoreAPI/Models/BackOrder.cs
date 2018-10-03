using System;

namespace bikestoreAPI.Models
{
    public class BackOrder
    {
        public int Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public DateTime DateExpected { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}