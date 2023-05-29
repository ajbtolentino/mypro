using System;
using MyPro.App.Application.Contracts.Repositories;
using MyPro.App.Domain.Contracts;

namespace MyPro.App.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
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

