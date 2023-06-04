using System;
using MyPro.App.Core.Entities;

namespace MyPro.App.Core.Entities
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}

