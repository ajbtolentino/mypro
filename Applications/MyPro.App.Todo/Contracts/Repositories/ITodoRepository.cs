using System;
using MyPro.App.Application.Contracts.Repositories;

namespace MyPro.App.Todo.Data.Repositories
{
    internal interface ITodoRepository : IGenericRepository<MyPro.App.Todo.Core.Entities.Todo>
    {
        public IEnumerable<MyPro.App.Todo.Core.Entities.Todo> GetAllActive();
    }
}

