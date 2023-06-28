using Microsoft.EntityFrameworkCore;
using MyPro.App.Infrastructure.Extensions;
using MyPro.Identity.Api.Infrastructure.Contracts.DbContext;
using MyPro.Identity.Api.Infrastructure.Contracts.Repositories;
using MyPro.Identity.Api.Infrastructure.Contracts.Services;
using MyPro.Identity.Api.Infrastructure.DbContext;
using MyPro.Identity.Api.Infrastructure.Repositories;
using MyPro.Identity.Api.Infrastructure.Services;

namespace MyPro.Identity.Api.Infrastructure.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration)
                    .AddIdentityDbContext(configuration)
                    .AddIdentityRepositories(configuration)
                    .AddIdentityServices(configuration);

            return services;
        }

        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //Add other dbconnection string support
            services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("IdentityDb"))
                    .AddScoped<IIdentityDbContext>(provider => provider.GetRequiredService<IdentityDbContext>());

            return services;
        }

        public static IServiceCollection AddIdentityRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

