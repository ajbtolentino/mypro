using System;
using MyPro.App.Core.DbContexts;
using MyPro.App.Infrastructure.Repositories;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.Repositories;

namespace MyPro.Todo.Infrastructure.Repositories
{
    internal class TodoRepository : GenericRepository<ITodoDbContext, Entities.Todo, int>, ITodoRepository
    {
        public TodoRepository(ITodoDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Entities.Todo> GetAllActive()
        {
            return new List<Entities.Todo>();
        }
    }
}

