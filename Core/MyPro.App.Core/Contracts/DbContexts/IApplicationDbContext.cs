using System;
namespace MyPro.App.Core.Contracts.DbContexts
{
    internal interface IApplicationDbContext
    {
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        public TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        public TEntity Update<TEntity>(TEntity entity) where TEntity : class;
        public void Delete<TEntity, TKey>(TKey id) where TEntity : class where TKey : struct;
        int SaveChanges();
    }
}

