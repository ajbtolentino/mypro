using System;
using System.Collections.Generic;

namespace MyPro.App.Core.DbContexts
{
    public interface IApplicationDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

