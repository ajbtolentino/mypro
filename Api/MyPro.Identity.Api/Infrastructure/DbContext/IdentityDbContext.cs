using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyPro.Identity.Api.Infrastructure.Contracts.DbContext;

namespace MyPro.Identity.Api.Infrastructure.DbContext
{
    internal class IdentityDbContext : Microsoft.EntityFrameworkCore.DbContext, IIdentityDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entities.User> Users => base.Set<Entities.User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

