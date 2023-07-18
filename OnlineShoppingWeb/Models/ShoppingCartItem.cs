using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingWeb.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }

        public string ShoppingCartId { get; set; }

    }
}
