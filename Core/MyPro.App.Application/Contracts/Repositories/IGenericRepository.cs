using System;
using MyPro.App.Domain.Contracts;

namespace MyPro.App.Application.Contracts.Repositories
{
    internal interface IGenericRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
        TEntity Add(TEntity entity);
        void Delete(TKey id);
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity entity);
    }

    internal interface IGenericRepository<TEntity>
        where TEntity : IEntity<int>
    {

    }
}

