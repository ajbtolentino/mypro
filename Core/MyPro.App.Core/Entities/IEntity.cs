using System;

namespace MyPro.App.Core.Entities
{
    internal interface IEntity<TKey>
        where TKey : struct
    {
        TKey Id { get; set; }
    }
}

