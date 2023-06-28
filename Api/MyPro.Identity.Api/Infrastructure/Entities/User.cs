using System;
using MyPro.App.Core.Entities;

namespace MyPro.Identity.Api.Infrastructure.Entities
{
    public class User : BaseEntity, IEntity<int>
    {
        public string Username { get; set; } = string.Empty;
    }
}

