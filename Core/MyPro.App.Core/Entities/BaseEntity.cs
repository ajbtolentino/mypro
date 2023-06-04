using System;
using MyPro.App.Core.Contracts.Entities;

namespace MyPro.App.Core.Entities
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}

