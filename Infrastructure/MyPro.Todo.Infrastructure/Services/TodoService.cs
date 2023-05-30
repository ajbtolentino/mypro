using System;
using MyPro.App.Core.Contracts.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.Repositories;
using MyPro.Todo.Infrastructure.Contracts.Services;

namespace MyPro.Todo.Infrastructure.Services
{
    internal class TodoService : ITodoService
    {
        private IApplicationDbContext dbContext;

        public TodoService(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(string text)
        {
            this.dbContext.Add<Entities.Todo>(new Entities.Todo());
            this.dbContext.SaveChanges();
        }

        public int Count()
        {
            return this.dbContext.GetAll<Entities.Todo>().Count();
        }
    }
}

