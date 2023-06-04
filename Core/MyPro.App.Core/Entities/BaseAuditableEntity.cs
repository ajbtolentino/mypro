using System;
using MyPro.App.Core.Entities;

namespace MyPro.App.Core.Entities
{
    internal class BaseAuditableEntity : BaseEntity, IAuditableEntity<int>
    {

    }
}

