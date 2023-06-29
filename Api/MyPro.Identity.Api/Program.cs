using IdentityServer;
using IdentityServer4;
using IdentityServerHost.Quickstart.UI;
using Microsoft.IdentityModel.Tokens;
using MyPro.Identity.Api.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users)
                .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
    .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.SignOutScheme = IdentityServerConstants.SignoutScheme;
        options.SaveTokens = true;

        options.Authority = "https://demo.identityserver.io/";
        options.ClientId = "interactive.confidential";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

