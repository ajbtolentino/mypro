using System;

namespace MyPro.App.Core.Entities
{
    internal interface IAuditableEntity<TKey> : IEntity<TKey>
        where TKey : struct
    {
    }
}

