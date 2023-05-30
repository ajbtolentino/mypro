using System;
using MyPro.App.Core.Contracts.DbContexts;
using MyPro.App.Core.Contracts.Entities;
using MyPro.App.Core.Contracts.Repositories;

namespace MyPro.App.Infrastructure.Repositories
{
    internal class GenericRepository<TDbContext, TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TDbContext : IApplicationDbContext
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
        protected readonly TDbContext dbContext;

        public GenericRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

