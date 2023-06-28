using System;
namespace MyPro.Identity.Api.Infrastructure.Contracts.Services
{
    public interface IUserService
    {
        Task Add(Entities.User user);
        IEnumerable<Entities.User> GetAll();
    }
}

