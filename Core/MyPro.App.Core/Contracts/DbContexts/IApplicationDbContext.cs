using System;
namespace MyPro.App.Core.Contracts.DbContexts
{
    internal interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

