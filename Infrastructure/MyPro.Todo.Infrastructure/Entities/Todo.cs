using System;
using MyPro.App.Core.Contracts.Entities;
using MyPro.App.Core.Entities;

namespace MyPro.Todo.Infrastructure.Entities
{
    internal class Todo : BaseEntity, IEntity<int>
    {
    }
}