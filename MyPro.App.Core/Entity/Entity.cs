using System;
namespace MyPro.App.Core.Entity
{
    public class Entity<T> where T : struct
    {
        public T Id { get; set; }
    }

    public class Entity : Entity<int> { }
}

