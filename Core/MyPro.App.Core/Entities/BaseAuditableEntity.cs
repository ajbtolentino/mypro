using System;
using MyPro.App.Core.Contracts.Entities;

namespace MyPro.App.Domain.Entities
{
    internal class BaseAuditableEntity : BaseEntity, IAuditableEntity<int>
    {

    }
}

