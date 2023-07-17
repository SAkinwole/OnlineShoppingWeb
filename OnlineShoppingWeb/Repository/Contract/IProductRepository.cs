using Microsoft.AspNetCore.Mvc;

namespace OnlineShoppingWeb.Repository.Contract
{
    public class IProductRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
