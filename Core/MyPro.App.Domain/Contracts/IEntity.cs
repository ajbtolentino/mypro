using System;
namespace MyPro.App.Domain.Contracts
{
    public interface IEntity<TKey>
        where TKey : struct
    {
        TKey Id { get; set; }
    }

    public interface IEntity : IEntity<int>
    {

    }
}

