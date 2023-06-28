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
              .AddInMemoryClients(Config.Clients)
             .AddInMemoryApiScopes(Config.ApiScopes)
             .AddInMemoryIdentityResources(Config.IdentityResources)
             .AddTestUsers(Config.TestUsers)
             .AddDeveloperSigningCredential();

            return services;
        }
    }

    public class Config
    {
        public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
             {
                 ClientId = "shopping-api",
                 ClientName = "Shopping Web App",
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 AllowRememberConsent = false,
                 AllowOfflineAccess = true,
                 RedirectUris = new List<string>()
                     {
                        "https://localhost:7237/signin-oidc"
                     },
                 PostLogoutRedirectUris = new List<string>()
                     {
                        "https://localhost:7237/signout-callback-oidc"
                     },
                 ClientSecrets =
                 {
                    new Secret("secret".Sha256())
                 },
                AllowedScopes = {
                     IdentityServerConstants.StandardScopes.OpenId,
                     IdentityServerConstants.StandardScopes.Profile,
                     "shopping-api"
                }
             }
        };
        public static IEnumerable<ApiScope> ApiScopes =>
         new ApiScope[]
         {
             new ApiScope("shopping-api", "Shopping API")
         };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
         {
         };
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
        };
        public static List<TestUser> TestUsers =>
         new List<TestUser>
         {
 new TestUser
 {
 SubjectId = "5BE86359–073C-434B-AD2D-A3932222DABE",
 Username = "allan",
 Password = "1234",
 Claims = new List<Claim>
 {
 new Claim(JwtClaimTypes.GivenName, "Allan"),
 new Claim(JwtClaimTypes.FamilyName, "Tolentino")
 }
 }
         };
    }
}

