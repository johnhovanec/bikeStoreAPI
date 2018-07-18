namespace bikestoreAPI.Models
{
    public class ShoppingCartProduct
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal? UnitPrice { get; set; }

        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}