using System;
using MyPro.App.Core.DbContexts;
using MyPro.App.Core.Entities;
using MyPro.App.Core.Repositories;

namespace MyPro.App.Infrastructure.Repositories
{
    internal class GenericRepository<TDbContext, TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TDbContext : IApplicationDbContext
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        protected readonly TDbContext dbContext;

        public GenericRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            var result = this.dbContext.Add(entity);

            this.dbContext.SaveChanges();

            return result;
        }

        public void Delete(TKey id)
        {
            this.dbContext.Delete<TEntity, TKey>(id);
        }

        public TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.dbContext.GetAll<TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            return this.dbContext.Update(entity);
        }
    }
}

