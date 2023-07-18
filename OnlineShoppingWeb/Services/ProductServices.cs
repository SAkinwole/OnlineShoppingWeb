using Microsoft.EntityFrameworkCore;
using OnlineShoppingWeb.Data;
using OnlineShoppingWeb.Models;

namespace OnlineShoppingWeb.Services
{
    public class ProductServices : IProductService
    {
        private readonly AppDbContext _context;
        public ProductServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(n => n.Id == id);

            return product;
        }
    }
}
