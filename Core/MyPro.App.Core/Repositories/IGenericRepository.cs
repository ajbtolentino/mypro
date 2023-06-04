﻿using System;
using MyPro.App.Core.Entities;

namespace MyPro.App.Core.Repositories
{
    internal interface IGenericRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
        TEntity Add(TEntity entity);
        void Delete(TKey id);
        TEntity Get(TKey id);
        IQueryable<TEntity> GetAll();
        TEntity Update(TEntity entity);
    }
}
