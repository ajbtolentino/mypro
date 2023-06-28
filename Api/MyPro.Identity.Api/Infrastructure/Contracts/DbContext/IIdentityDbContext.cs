using Microsoft.EntityFrameworkCore;
using MyPro.App.Core.DbContexts;

namespace MyPro.Identity.Api.Infrastructure.Contracts.DbContext
{
    internal interface IIdentityDbContext : IApplicationDbContext
    {
        DbSet<Entities.User> Users { get; }
    }
}

