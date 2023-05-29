using System;
using MyPro.App.Domain.Contracts;

namespace MyPro.Todo.Infrastructure.Entities
{
    internal class Todo : IEntity<int>
    {
        public int Id { get; set; }
    }
}

