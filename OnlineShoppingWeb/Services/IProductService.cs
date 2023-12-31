﻿using OnlineShoppingWeb.Models;

namespace OnlineShoppingWeb.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProduct(int? id);
    }
}
