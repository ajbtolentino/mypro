using System;
using MyPro.App.Core.Contracts.Entities;

namespace MyPro.App.Core.Entities
{
    internal class BaseAuditableEntity : BaseEntity, IAuditableEntity<int>
    {

    }
}

