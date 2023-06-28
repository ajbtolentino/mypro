using System;
using MyPro.Identity.Api.Infrastructure.Contracts.Repositories;
using MyPro.Identity.Api.Infrastructure.Contracts.Services;
using MyPro.Identity.Api.Infrastructure.Entities;

namespace MyPro.Identity.Api.Infrastructure.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Add(User user)
        {
            await this.userRepository.AddAsync(user);
        }

        public IEnumerable<User> GetAll()
        {
            return this.userRepository.GetAll();
        }
    }
}

