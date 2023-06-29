using IdentityModel;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.EntityFrameworkCore;
using MyPro.App.Infrastructure.Extensions;
using MyPro.Identity.Api.Infrastructure.Contracts.DbContext;
using MyPro.Identity.Api.Infrastructure.Contracts.Repositories;
using MyPro.Identity.Api.Infrastructure.Contracts.Services;
using MyPro.Identity.Api.Infrastructure.DbContext;
using MyPro.Identity.Api.Infrastructure.Repositories;
using MyPro.Identity.Api.Infrastructure.Services;
using static System.Net.WebRequestMethods;
using IdentityServer;
using IdentityServerHost.Quickstart.UI;

namespace MyPro.Identity.Api.Infrastructure.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration)
                    .AddIdentityDbContext(configuration)
                    .AddIdentityRepositories(configuration)
                    .AddIdentityServices(configuration)
                    .AddIdentityServer(configuration);

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

        public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
             .AddTestUsers(TestUsers.Users)
             .AddDeveloperSigningCredential();

            return services;
        }
    }
}

