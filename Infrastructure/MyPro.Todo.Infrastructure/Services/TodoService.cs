using System;
using MyPro.App.Core.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.DbContexts;
using MyPro.Todo.Infrastructure.Contracts.Repositories;
using MyPro.Todo.Infrastructure.Contracts.Services;
using MyPro.Todo.Infrastructure.DbContexts;

namespace MyPro.Todo.Infrastructure.Services
{
    internal class TodoService : ITodoService
    {
        private ITodoRepository todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public async Task AddAsync(string text)
        {
            await this.todoRepository.AddAsync(new Entities.Todo());
        }

        public int Count()
        {
            return this.todoRepository.GetAll().Count();
        }
    }
}

