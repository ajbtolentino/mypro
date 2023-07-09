using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.Catalog.Api.Entities;

namespace MyPro.Catalog.Api.DbContext
{
    public class CatalogDbContext : ApplicationDbContext, ICatalogDbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }

    public static class CatalogDbContextSeeder
    {
        public static void Seed(CatalogDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            for (int i = 1; i < 10; i++)
            {
                dbContext.Categories.Add(new Category
                {
                    Id = i,
                    Name = $"Category - {i}"
                });
            }

            for (int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = $"Product {i} - Category {i}",
                    Description = "Product Description",
                    Category = i,
                    Price = i*10,
                    Rating = 4M
                });
            }

            dbContext.SaveChanges();
        }
    }
}

