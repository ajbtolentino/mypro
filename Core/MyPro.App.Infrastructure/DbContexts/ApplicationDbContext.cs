using System;
using Microsoft.EntityFrameworkCore;
using MyPro.App.Core.DbContexts;

namespace MyPro.App.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}

