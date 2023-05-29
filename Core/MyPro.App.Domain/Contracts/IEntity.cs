using System;

namespace MyPro.App.Domain.Contracts
{
    internal interface IEntity<TKey>
        where TKey : struct
    {
        TKey Id { get; set; }
    }
}

