using System;
using System.Collections.Generic;

namespace MyPro.App.Core.DbContexts
{
    internal interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

