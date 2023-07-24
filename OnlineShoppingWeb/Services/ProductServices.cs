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
            var product = await _context.Products.FindAsync(id);

            return product;
        }

        public async Task<Product> GetProduct(int? id)
        {

            if (id == null || _context.Products == null)
            {
                return null ;
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return null;
            }

            return product;
        }
    }
}
