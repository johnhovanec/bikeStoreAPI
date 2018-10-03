using System.Collections.Generic;

namespace bikestoreAPI.Models
{
    public class ShippingMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public string Restrictions { get; set; }

        //
        public List<Order> Order { get; set; }
    }
}
