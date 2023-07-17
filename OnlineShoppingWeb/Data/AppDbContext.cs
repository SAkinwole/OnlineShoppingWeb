﻿using Microsoft.EntityFrameworkCore;
using OnlineShoppingWeb.Models;

namespace OnlineShoppingWeb.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}