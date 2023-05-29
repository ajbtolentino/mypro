using System;
using MyPro.App.Application.Contracts.Repositories;

namespace MyPro.Todo.Infrastructure.Contracts.Repositories
{
    internal interface ITodoRepository : IGenericRepository<MyPro.Todo.Infrastructure.Entities.Todo, int>
    {
        public IEnumerable<MyPro.Todo.Infrastructure.Entities.Todo> GetAllActive();
    }
}

