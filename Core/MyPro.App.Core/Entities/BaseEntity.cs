﻿using System;
using MyPro.App.Core.Contracts.Entities;

namespace MyPro.App.Domain.Entities
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}

