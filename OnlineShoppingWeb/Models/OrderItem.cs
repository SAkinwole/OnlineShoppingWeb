namespace OnlineShoppingWeb.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Order Order { get; set; }
    }
}
