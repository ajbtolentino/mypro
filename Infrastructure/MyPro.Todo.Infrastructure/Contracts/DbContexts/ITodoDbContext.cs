using System;
using Microsoft.EntityFrameworkCore;
using MyPro.App.Core.DbContexts;

namespace MyPro.Todo.Infrastructure.Contracts.DbContexts
{
    internal interface ITodoDbContext : IApplicationDbContext
    {
        DbSet<Entities.Todo> Todos { get; }
    }
}

