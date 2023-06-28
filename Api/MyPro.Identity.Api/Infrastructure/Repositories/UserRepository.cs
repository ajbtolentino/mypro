using MyPro.App.Infrastructure.Repositories;
using MyPro.Identity.Api.Infrastructure.Contracts.Repositories;
using MyPro.Identity.Api.Infrastructure.DbContext;

namespace MyPro.Identity.Api.Infrastructure.Repositories
{
    internal sealed class UserRepository : GenericRepository<Entities.User, int>, IUserRepository
    {
        public UserRepository(IdentityDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Entities.User> GetAllActive()
        {
            return new List<Entities.User>();
        }
    }
}

