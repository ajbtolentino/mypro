using System;

namespace MyPro.App.Core.Contracts.Entities
{
    internal interface IAuditableEntity<TKey> : IEntity<TKey>
        where TKey : struct
    {
    }
}

