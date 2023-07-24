using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingWeb.Data;
using OnlineShoppingWeb.Models;
using OnlineShoppingWeb.Services;
using OnlineShoppingWeb.ViewModel;
using System.Security.Claims;

namespace OnlineShoppingWeb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly IOrdersService _ordersService;
        public OrdersController(ShoppingCart shoppingCart, IProductService productService, AppDbContext context, IOrdersService ordersService)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
            _context = context;
            _ordersService = ordersService;
        }


        public async Task<IActionResult> Index()
        {
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userRole = User.FindFirstValue(ClaimTypes.Role);

            string userId = "";

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var products = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = products;

            var response = new ShoppingCartVM
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddProductToShoppingCart(int? id)
        {

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            //var product = await _productService.GetProduct(id);
            if (product != null)
            {
                _shoppingCart.AddProductToCart(product);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }
        public async Task<IActionResult> RemoveProductFromShoppingCart(int? id)
        {
            var product = await _productService.GetProduct(id);
            if (product != null)
            {
                _shoppingCart.RemoveProductFromCart(product);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

    }
}
