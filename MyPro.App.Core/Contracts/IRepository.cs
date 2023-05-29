using System;
using MyPro.App.Core.Entity;

namespace MyPro.App.Core.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    internal interface IRepository<TEntity, TKey>
        where TEntity: Entity<TKey>
        where TKey : struct
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TKey id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    internal interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : Entity<int>
    {

    }
}

