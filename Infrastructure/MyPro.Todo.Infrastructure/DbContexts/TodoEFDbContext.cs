using System;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;

namespace MyPro.Todo.Infrastructure.DbContexts
{
    internal class TodoEFDbContext : EFDbContext, ITodoDbContext
    {
        public TodoEFDbContext()
        {
        }
    }
}

