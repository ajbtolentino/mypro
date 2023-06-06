using System;
namespace MyPro.Todo.Infrastructure.Contracts.Services
{
    public interface ITodoService
    {
        Task AddAsync(string text);

        int Count();
    }
}

