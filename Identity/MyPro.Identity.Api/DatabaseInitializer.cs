using IdentityServer;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPro.Identity.Api.Models;

namespace MyPro.Identity.Api
{

    public static class DatabaseInitializer
    {
        public static async Task PopulateIdentityServer(IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            if (serviceScopeFactory == null)
                throw new NullReferenceException(nameof(app));

            using var serviceScope = serviceScopeFactory.CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Database.Migrate();

            foreach (var client in Config.Clients)
            {
                var item = context.Clients.SingleOrDefault(c => c.ClientId == client.ClientId);

                if (item == null)
                {
                    context.Clients.Add(client.ToEntity());
                }
            }

            foreach (var resource in Config.ApiResources)
            {
                var item = context.ApiResources.SingleOrDefault(c => c.Name == resource.Name);

                if (item == null)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
            }

            foreach (var resource in Config.IdentityResources)
            {
                var item = context.IdentityResources.SingleOrDefault(c => c.Name == resource.Name);

                if (item == null)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
            }

            foreach (var scope in Config.ApiScopes)
            {
                var item = context.ApiScopes.SingleOrDefault(c => c.Name == scope.Name);

                if (item == null)
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
            }

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var user in Config.Users)
            {
                var item = userManager.Users.FirstOrDefault(_ => _.UserName == user.Username);

                if(item == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = user.Username,
                        Email = user.Username,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newUser, user.Password);
                    var u = userManager.Users.FirstOrDefault(_ => _.UserName == user.Username);

                    await userManager.AddClaimsAsync(u, user.Claims);
                }
            }

            context.SaveChanges();
        }
    }
}

