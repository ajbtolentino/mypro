using System;
using MyPro.App.Core.Contracts.Entities;
using MyPro.App.Domain.Entities;

namespace MyPro.Todo.Infrastructure.Entities
{
    internal class Todo : BaseEntity, IEntity<int>
    {
    }
}