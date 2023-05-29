using System;
using MyPro.App.Infrastructure.Repositories;
using MyPro.App.Todo.Data.Repositories;

namespace MyPro.App.Todo.Repositories
{
    internal class TodoRepository : GenericRepository<MyPro.App.Todo.Core.Entities.Todo>, ITodoRepository
    {
        public TodoRepository()
        {
        }

        public IEnumerable<Core.Entities.Todo> GetAllActive()
        {
            throw new NotImplementedException();
        }
    }
}

