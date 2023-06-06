using System;
using MyPro.App.Infrastructure.Repositories;
using MyPro.Todo.Infrastructure.Contracts.Repositories;
using MyPro.Todo.Infrastructure.DbContexts;

namespace MyPro.Todo.Infrastructure.Repositories
{
    internal class TodoRepository : GenericRepository<Entities.Todo, int>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Entities.Todo> GetAllActive()
        {
            return new List<Entities.Todo>();
        }
    }
}

