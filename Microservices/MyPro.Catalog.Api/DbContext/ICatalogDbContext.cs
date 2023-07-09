using System;
using Microsoft.EntityFrameworkCore;
using MyPro.App.Core.DbContexts;
using MyPro.Catalog.Api.Entities;

namespace MyPro.Catalog.Api.DbContext
{
    public interface ICatalogDbContext : IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
    }
}

