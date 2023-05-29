using System;
using MyPro.App.Application.Contracts.DbContexts;
using MyPro.App.Infrastructure.Repositories;
using MyPro.Todo.Infrastructure.Contracts.Repositories;

namespace MyPro.Todo.Infrastructure.Repositories
{
    internal class TodoRepository : GenericRepository<Entities.Todo, int>, ITodoRepository
    {
        public TodoRepository(IApplicationDbContext dbContext)
        {
        }

        public IEnumerable<Entities.Todo> GetAllActive()
        {
            throw new NotImplementedException();
        }
    }
}

