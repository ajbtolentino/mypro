using System;
using Microsoft.EntityFrameworkCore;
using MyPro.App.Core.Contracts.DbContexts;

namespace MyPro.App.Infrastructure.DbContexts
{
    internal class EFDbContext : DbContext, IApplicationDbContext
    {
        public EFDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Add<TEntity>(entity).Entity;
        }

        public new TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Update<TEntity>(entity).Entity;
        }

        public void Delete<TEntity, TKey>(TKey id)
            where TEntity : class
            where TKey : struct
        {
            var entity = base.Find<TEntity>(id);

            if(entity != null) base.Remove<TEntity>(entity);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}

