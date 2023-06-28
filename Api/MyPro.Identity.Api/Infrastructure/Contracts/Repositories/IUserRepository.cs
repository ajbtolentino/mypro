using System;
using MyPro.App.Core.Repositories;

namespace MyPro.Identity.Api.Infrastructure.Contracts.Repositories
{
    internal interface IUserRepository : IGenericRepository<int, Entities.User>
    {
        public IEnumerable<Entities.User> GetAllActive();
    }
}

