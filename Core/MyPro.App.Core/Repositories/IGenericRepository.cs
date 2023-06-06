using System;
using MyPro.App.Core.Entities;

namespace MyPro.App.Core.Repositories
{
    internal interface IGenericRepository<TKey, TEntity>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        Task<TEntity> AddAsync(TEntity entity);
        void Delete(TKey id);
        TEntity GetById(TKey id);
        IQueryable<TEntity> GetAll();
        TEntity Update(TEntity entity);
    }
}

