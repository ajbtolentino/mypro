using System;

namespace MyPro.App.Core.Contracts.Entities
{
    internal interface IEntity<TKey>
        where TKey : struct
    {
        TKey Id { get; set; }
    }
}

