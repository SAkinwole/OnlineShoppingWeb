using Microsoft.EntityFrameworkCore;
using OnlineShoppingWeb.Models;

namespace OnlineShoppingWeb.Data
{
    public class ShoppingCart
    {
        public AppDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);


            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(x => x.ShoppingCartId ==
                        ShoppingCartId).Include(n => n.Product).ToList());
        }
        public void AddProductToCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id &&
              n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }
            _context.SaveChanges();
        }

        public void RemoveProductFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id &&
                n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public double GetShoppingCartTotal() =>_context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
                                                        .Select(n => n.Product.Price * n.Qty).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
