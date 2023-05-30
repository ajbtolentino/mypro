﻿using System;
using MyPro.App.Core.Contracts.Repositories;

namespace MyPro.Todo.Infrastructure.Contracts.Repositories
{
    internal interface ITodoRepository : IGenericRepository<Entities.Todo, int>
    {
        public IEnumerable<Entities.Todo> GetAllActive();
    }
}

