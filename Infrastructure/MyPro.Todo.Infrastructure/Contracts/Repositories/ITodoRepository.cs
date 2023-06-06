using System;
using MyPro.App.Core.Repositories;

namespace MyPro.Todo.Infrastructure.Contracts.Repositories
{
    internal interface ITodoRepository : IGenericRepository<int, Entities.Todo>
    {
        public IEnumerable<Entities.Todo> GetAllActive();
    }
}

