using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPro.Identity.Api;
using MyPro.Identity.Api.DbContext;
using MyPro.Identity.Api.Models;

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

builder.Services.AddAuthentication();

var app = builder.Build();

// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}

DatabaseInitializer.PopulateIdentityServer(app).Wait();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.UseCors(configure =>
{
    configure.AllowAnyHeader();
    configure.AllowAnyMethod();
    configure.AllowAnyOrigin();
});

app.Run();

