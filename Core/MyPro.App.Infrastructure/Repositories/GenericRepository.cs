using System;
using Microsoft.EntityFrameworkCore;
using MyPro.App.Core.Entities;
using MyPro.App.Core.Repositories;

namespace MyPro.App.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TKey, TEntity>
        where TKey : struct
        where TEntity : class, IEntity<TKey>
    {
        protected readonly DbContext dbContext;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new NullReferenceException(nameof(dbContext));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await this.dbContext.AddAsync(entity);

            this.dbContext.SaveChanges();

            return result.Entity;
        }

        public void Delete(TKey id)
        {
            var entity = this.dbContext.Set<TEntity>().Find(id);

            this.dbContext.Remove<TEntity>(entity!);

            this.dbContext.SaveChanges();
        }

        public TEntity GetById(TKey id)
        {
            return this.dbContext.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.dbContext.Set<TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            return this.dbContext.Set<TEntity>().Update(entity).Entity;
        }
    }
}

