using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyPro.Identity.Api.DbContext;
//using MyPro.Identity.Api.Infrastructure.Extensions;
using MyPro.Identity.Api.Models;
using Microsoft.EntityFrameworkCore;
using MyPro.Identity.Api;
using System;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
var migrationAssembly = "MyPro.Identity.Api";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
                });

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://localhost:5001/";
                    options.ClientId = "private-api";
                    options.ClientSecret = "secret";
                    options.ResponseType = "id_token";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

var app = builder.Build();

// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}

Task.WaitAll(DatabaseInitializer.PopulateIdentityServer(app));

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

