using System;
namespace MyPro.Todo.Infrastructure.Contracts.Services
{
    public interface ITodoService
    {
        void Add(string text);

        int Count();
    }
}

