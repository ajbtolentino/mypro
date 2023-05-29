using System;
using MyPro.App.Infrastructure.DbContexts;
using MyPro.App.Todo.Contracts.DbContexts;

namespace MyPro.App.Todo.Infrastructure.Data.Todo
{
    internal class TodoEFDbContext : EFDbContext, ITodoDbContext
    {
        public TodoEFDbContext()
        {
        }
    }
}

