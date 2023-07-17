namespace OnlineShoppingWeb.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
      //  public PaymentMethod PaymentMethod { get; set; }
    }
}
