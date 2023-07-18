using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingWeb.Data;
using OnlineShoppingWeb.Models;
using OnlineShoppingWeb.Services;
using OnlineShoppingWeb.ViewModel;

namespace OnlineShoppingWeb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IProductService _productService;
        public OrdersController(ShoppingCart shoppingCart, IProductService productService)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
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

        public async Task<IActionResult> AddProductToCart(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.AddItemToCart(product);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }
        public async Task<IActionResult> RemoveProductFromCart(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.RemoveItemFromCart(product);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
