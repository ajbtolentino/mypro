using System;
using MyPro.App.Core.Contracts.DbContexts;

namespace MyPro.App.Infrastructure.DbContexts
{
    internal class EFDbContext : IApplicationDbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

